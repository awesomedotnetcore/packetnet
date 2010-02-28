/*
This file is part of PacketDotNet

PacketDotNet is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

PacketDotNet is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with PacketDotNet.  If not, see <http://www.gnu.org/licenses/>.
*/
/*
 *  Copyright 2010 Chris Morgan <chmorgan@gmail.com>
 */

using System;
using NUnit.Framework;
using SharpPcap;
using PacketDotNet;

namespace Test
{
    [TestFixture]
    public class IpPacketTest
    {
        /// <summary>
        /// Test that parsing an ip packet yields the proper field values
        /// </summary>
        [Test]
        public void IpPacketFields()
        {
            var dev = new OfflinePcapDevice("../../CaptureFiles/tcp.pcap");
            dev.Open();
            var rawPacket = dev.GetNextRawPacket();
            dev.Close();

            Packet p = Packet.ParsePacket((LinkLayers)rawPacket.LinkLayerType,
                                          new PosixTimeval(rawPacket.Timeval.Seconds,
                                                           rawPacket.Timeval.MicroSeconds),
                                          rawPacket.Data);

            Assert.IsNotNull(p);

            var ip = IpPacket.GetType(p);
            Console.WriteLine(ip.GetType());

            Assert.AreEqual(20, ip.Header.Length, "Header.Length doesn't match expected length");
            Console.WriteLine(ip.ToString());
        }
    }
}
