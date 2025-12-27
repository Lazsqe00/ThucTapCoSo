from collections import defaultdict, namedtuple

Edge = namedtuple("Edge", ["u", "v", "w"])

def mst_steps(nodes, edges, start_node):
    #B1

    _nodes = len(nodes)
    mst_edges = []
    steps = []
    pop_count = 0  
    
    #B2
    edge_w = {}
    adj = defaultdict(set)
    for u, v, w in edges:
        adj[u].add(v)
        adj[v].add(u)
        edge_w[(u, v)] = edge_w[(v, u)] = w
    
    #B3

    visited = {start_node}
    steps.append(("init_mst", [], 0, None, set(), "Khởi tạo MST := empty."))
    steps.append(("init", [], 1, None, set(visited), 
    f"Khởi tạo visited = {{{start_node}}}."))
    
    #B4
    pq = []
    for v in adj[start_node]:
        pq.append((edge_w[(start_node, v)], start_node, v))
    steps.append(("push_init", [], 2, adj[start_node], set(visited),
        f"Đỉnh {start_node} đẩy vào PQ các cạnh: " +
        ", ".join([f"({start_node},{v}) w={edge_w[(start_node,v)]}" for v in adj[start_node]])))
    
    #B5
    while len(visited) < _nodes:
        pq.sort()
        w, u, v = pq.pop(0)
        pop_count += 1
        steps.append(("pop", list(mst_edges), 4, {u}, set(visited), 
            f"Chọn cạnh nhỏ nhất: ({u},{v}) w={w}."))
        
        #B6
        if v in visited:
            steps.append(("skip", list(mst_edges), 5, None, set(visited), 
            f"Đỉnh {v} đã thăm, bỏ qua cạnh ({u},{v})."))
            continue
        
        #B7
        mst_edges.append(Edge(u, v, w))
        steps.append(("add_mst", list(mst_edges), 6, None, set(visited), 
        f"Thêm cạnh ({u},{v}) w={w} vào MST."))
        steps.append(("visit", list(mst_edges), 7, {v}, set(visited), 
        f"Đánh dấu đã thăm đỉnh {v}."))
        visited.add(v)
        
        #B8
        neighbors = []
        new_adj = []
        for x in adj[v]:
            if x not in visited:
                pq.append((edge_w[(v, x)], v, x))
                new_adj.append(x)
                neighbors.append(f"({v},{x}) w={edge_w[(v,x)]}")
        steps.append(("push_new", list(mst_edges), 8, new_adj, set(visited),
                      "Các cạnh mới được đẩy vào PQ: " + (", ".join(neighbors) 
                      if neighbors else "Không có.")))
    #B9
    total_weight = sum(e.w for e in mst_edges)
    conclusion = (
        "Hoàn tất thuật toán Prim. "
        f"- Số cạnh đã duyệt: {pop_count}. "
        f"- Tổng trọng số của MST: {total_weight}."
    )
    steps.append(("done", list(mst_edges), 9, None, set(visited), conclusion))
    
    #B10-end
    return steps