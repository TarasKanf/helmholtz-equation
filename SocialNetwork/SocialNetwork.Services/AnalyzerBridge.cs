using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Services
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ConnectionString
    {
        public string Id1;

        public string Id2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Mystring
    {
        public string Id;
    }

    public static class AnalyzerBridge
    {
        public static Guid[] GetMutualFriends(Connection[] connections, Guid userId1, Guid userId2)
        {
            if (userId1 == userId2)
            {
                return null;
            }
               
            int size;
            var stringConnection = GetConnectionString(connections);
            var pointer = FindMutualFriends(
                stringConnection,
                stringConnection.Length,
                userId1.ToString(),
                userId2.ToString(),
                out size);

            return GetGuidArray(pointer, size);
        }

        public static Guid[] GetShortestPath(Connection[] connections, Guid userId1, Guid userId2)
        {
            int size;
            var stringConnection = GetConnectionString(connections);
            var pointer = FindShortestPath(
                stringConnection,
                stringConnection.Length,
                userId1.ToString(),
                userId2.ToString(),
                out size);

            return GetGuidArray(pointer, size);
        }

        [DllImport("SocialNetwork.Analizer.dll", EntryPoint = "findMutualFriends")]
        private static extern IntPtr FindMutualFriends(
            ConnectionString[] structs,
            int items,
            string userId1,
            string userId2,
            out int resultArraySize);

        [DllImport("SocialNetwork.Analizer.dll", EntryPoint = "findShortestPath")]
        private static extern IntPtr FindShortestPath(
            ConnectionString[] structs,
            int items,
            string userId1,
            string userId2,
            out int resultArraySize);

        private static ConnectionString[] GetConnectionString(Connection[] connections)
        {
            var stringConnection = new ConnectionString[connections.Length];
            for (var i = 0; i < connections.Length; ++i)
            {
                stringConnection[i].Id1 = connections[i].User1Id.ToString();
                stringConnection[i].Id2 = connections[i].User2Id.ToString();
            }

            return stringConnection;
        }

        private static Guid[] GetGuidArray(IntPtr pointer, int size)
        {
            var counter = 0;
            var list = new List<Mystring>();
            int sizeOfPointer = Marshal.SizeOf<Mystring>();
            while (counter < size)
            {
                list.Add(Marshal.PtrToStructure<Mystring>(pointer));
                pointer += sizeOfPointer;
                counter++;
            }

            var result = new Guid[size];
            for (var i = 0; i < size; ++i)
            {
                result[i] = Guid.Parse(list[i].Id);
            }

            return result;
        }
    }
}