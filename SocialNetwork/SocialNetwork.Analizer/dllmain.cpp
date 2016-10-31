// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "SocialNetwork.Analizer.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}
extern "C"
{

	__declspec(dllexport) char** __stdcall findMutualFriends(Connection* pStructs,
		int nItems,
		const char* userId1,
		const char* userId2,
		int& resultArraySize)
	{
		vector<Connection> Connections;
		for (int i = 0; i < nItems; i++)
			Connections.push_back(pStructs[i]);
		Connection Users(userId1, userId2);
		vector<string> MutualFriends = Analizer::GetMutualFriends(Connections, Users);
		resultArraySize = MutualFriends.size();
		char** ArrMutualFriends = new char*[resultArraySize];
		for (int i = 0; i < resultArraySize; i++)
		{
			ArrMutualFriends[i] = new char[MutualFriends[i].length() + 1];
			for (int j = 0; j < MutualFriends[i].length(); j++)
				ArrMutualFriends[i][j] = MutualFriends[i].at(j);
			ArrMutualFriends[i][MutualFriends[i].length()] = '\0';
		}
		return ArrMutualFriends;
	}

	__declspec(dllexport) char** __stdcall findShortestPath(Connection* pStructs,
		int nItems,
		const char* userId1,
		const char* userId2,
		int& resultArraySize)
	{
		vector<Connection> Connections;
		for (int i = 0; i < nItems; i++)
			Connections.push_back(pStructs[i]);
		Connection Users(userId1, userId2);
		vector<string> ShortestPath = Analizer::GetShortestPath(Connections, Users);
		resultArraySize = ShortestPath.size();
		char** ArrIDPath = new char*[resultArraySize];
		for (int i = 0; i < resultArraySize; i++)
		{
			ArrIDPath[i] = new char[ShortestPath[i].length() + 1];
			for (int j = 0; j < ShortestPath[i].length(); j++)
				ArrIDPath[i][j] = ShortestPath[i].at(j);
			ArrIDPath[i][ShortestPath[i].length()] = '\0';
		}

		return ArrIDPath;
	}
}


