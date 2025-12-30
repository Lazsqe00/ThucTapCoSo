#include<bits/stdc++.h>
using namespace std;
using namespace chrono;
using ll = long long;
#define FOR(i,a,b) for(int i=a;i<=b;i++)
const int MAX = 1e5 + 5;

int N, M;
int par[MAX], sz[MAX];

struct edge{
	int u, v, w;
};

edge adj[8000000];

bool cmp(edge a, edge b){
	return a.w < b.w;
}

int find(int v){
	return v == par[v] ? v : v = find(par[v]);
}

void init_DSU(){
	FOR(i, 1, N){
		par[i] = i;
		sz[i] = 1;
	}
}

bool Union(int a, int b){
	a = find(a);
	b = find(b);
	if(a == b) return false;
	if(sz[a] < sz[b]) swap(a,b);
	par[b] = a;
	sz[a] += sz[b];
	return true;
}

long long kruskal(){
	sort(adj + 1, adj + M + 1, cmp);
	
	int cnt = 0;
	ll ans = 0;
	int cnt_edge = 0;
	
	FOR(i, 1, M){
		cnt_edge++;
		if(cnt == N - 1) break;
		if(Union(adj[i].u, adj[i].v)){
			ans += adj[i].w;
			cnt++;
		}
	}
	cout << "So canh duyet: " << cnt_edge << endl;
	if(cnt < N - 1) return -1;
	return ans;
}

int main(){
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	
	vector<string> files = {
		//canh day dac
		"graph_1000_80000.txt",
		"graph_2000_900000.txt",
		"graph_3000_4498500.txt",
		"graph_4000_7998000.txt",
		"graph_6000_3000000.txt",
		// canh thua
		"graph_1000_1200.txt",
		"graph_2000_3000.txt",
		"graph_4000_6000.txt",
		"graph_6000_8000.txt",
		"graph_8000_9000.txt",
	};
	
	for(string fileName : files){
		ifstream fin(fileName);
		if(!fin){
			cout << "Khong mo duoc file " << fileName << "\n";
			continue;
		}
		
		fin >> N >> M;
		
		FOR(i, 1, M){
			fin >> adj[i].u >> adj[i].v >> adj[i].w;
		}
		fin.close();
		
		init_DSU();
		
		auto start = high_resolution_clock::now();
		ll mst = kruskal();
		auto end = high_resolution_clock::now();
		
		duration<double, milli> time = end - start;
		
		cout << "File: " << fileName << "\n";
		cout << "N = " << N << ", M = " << M << "\n";
		

		cout << "MST Kruskal = " << mst << "\n";
		
		cout << "Thoi gian Kruskal: " << time.count() << " ms\n";
		cout << "--------------------------\n";
	}
	
	return 0;
}
