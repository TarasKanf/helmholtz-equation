#pragma once
#include "stdafx.h"
#include <iostream>
#include <vector>
#include <string>
#include <exception>
#include <map>

using std::string;
using std::vector;
using std::multimap;

class Connection
{
public:
	const char* userId1;
	const char* userId2;
	Connection(const char* id1, const char* id2)
	{
		userId1 = id1;
		userId2 = id2;
	}
};
class Analizer
{
public:
	static vector<string> GetMutualFriends(const vector<Connection> Connections, const Connection Users);
	static vector<string> GetShortestPath(const vector<Connection> Connections, const Connection Users);
private:
	static vector<string> GetFriends(const vector<Connection> AllConnections, const string UserId);
	static bool AreFriends(const string id1, const string id2, const vector<Connection> Connections);
	static vector<string> GetNextFriends(const vector<Connection> AllConnections, const vector<string> PrevFriends, const string UserId);
};

bool Analizer::AreFriends(const string id1, const string id2, const vector<Connection> Connections)
{
	int sizeArr = Connections.size();
	if (sizeArr == 0) return false;
	for (int i = 0; i < sizeArr; i++)
		if ((Connections[i].userId1 == id1 &&
			Connections[i].userId2 == id2) ||
			(Connections[i].userId1 == id2 &&
				Connections[i].userId2 == id1))
			return true;
	return false;
}

vector<string> Analizer::GetFriends(const vector<Connection> AllConnections, const string UserId)
{
	vector<string> FriendsOfUser;
	for (int i = 0; i < AllConnections.size(); i++)
	{
		if (AllConnections[i].userId1 == UserId)
			FriendsOfUser.push_back(AllConnections[i].userId2);
		if (AllConnections[i].userId2 == UserId)
			FriendsOfUser.push_back(AllConnections[i].userId1);
	}
	return FriendsOfUser;
}
vector<string> Analizer::GetMutualFriends(const vector<Connection> Connections, const Connection Users)
{
	vector<string> MutualFriends;
	vector<string> FriendsListUser1 = GetFriends(Connections, Users.userId1);
	for (int i = 0; i < FriendsListUser1.size(); i++)
	{
		if (AreFriends(Users.userId2, FriendsListUser1[i], Connections))
			MutualFriends.push_back(FriendsListUser1[i]);
	}
	return MutualFriends;
}
vector<string> Analizer::GetNextFriends(const vector<Connection> AllConnections, const vector<string> PrevFriends, const string UserId)
{
	vector<string> NextFriends;
	vector<string> AllFriends = GetFriends(AllConnections, UserId);
	for (int i = 0; i < AllFriends.size(); i++)
	{
		bool IsNextFriend = true;
		for (int j = 0; j < PrevFriends.size(); j++)
			if (AllFriends[i] == PrevFriends[j])
			{
				IsNextFriend = false; break;
			}
		if (IsNextFriend) NextFriends.push_back(AllFriends[i]);
	}
	return NextFriends;
}
vector<string> Analizer::GetShortestPath(const vector<Connection> AllConnections, const Connection Users)
{
	bool Exist;
	vector<string> Path;
	vector<string> ShortestPath;
	multimap<string, string> Nodes;
	Path.push_back(Users.userId1);
	Nodes.insert(std::pair<string, string>(Users.userId1, Users.userId1));
	for (int i = 0; i<Path.size(); i++)
	{
		vector<string> NextFriends = GetNextFriends(AllConnections, Path, Path[i]);
		for (int j = 0; j < NextFriends.size(); j++)
		{
			Path.push_back(NextFriends[j]);
			Nodes.insert(std::pair<string, string>(NextFriends[j], Path[i]));
		}
		if (Path[i] == Users.userId2)
		{
			Exist = true;
			ShortestPath.push_back(Path[i]);
			break;
		}
	}
	if (Exist)
	{
		do {
			string Key = ShortestPath.back();
			ShortestPath.push_back(Nodes.find(Key)->second);
		} while (ShortestPath.back() != Users.userId1);
		return ShortestPath;
	}
	else
		throw std::exception("The Shortest Path Doesn't Exist!");
}


