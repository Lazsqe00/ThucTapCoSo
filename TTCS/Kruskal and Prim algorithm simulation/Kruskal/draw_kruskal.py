import matplotlib.pyplot as plt
import networkx as nx
import matplotlib.patches as patches
from graph_loader import Edge

PSEUDOCODE = [
    "1. Sort all edges in increasing order of weight",
    "2. MST := empty",
    "3. Initialize disjoint set (each vertex in its own component)",
    "4. For each edge (u, v, w) in sorted order:",
    "5.     if u and v are not in the same component:",
    "6.         Add edge (u, v) to MST",
    "7.         Union the components containing u and v",
    "8. Return MST"
]

def draw_step(G, pos, step, pause=1.5):
    plt.clf()
    action, current_mst, code_line, highlight_nodes, log_text = step
    
    ax1 = plt.subplot(2, 1, 1)
    

    connected_nodes = set()
    for e in current_mst:
        connected_nodes.add(e.u)
        connected_nodes.add(e.v)
    node_colors = ["#ff8c00" if node in connected_nodes else "lightgray" for node in G.nodes]
    

    edge_colors = []
    edge_widths = []
    for u_v in G.edges:
        edge = Edge(u_v[0], u_v[1], G[u_v[0]][u_v[1]]['weight'])
        rev_edge = Edge(u_v[1], u_v[0], edge.w)
        
        if edge in current_mst or rev_edge in current_mst:
            edge_colors.append('red')
            edge_widths.append(2.0)
        elif highlight_nodes and set(u_v) == highlight_nodes:
            edge_colors.append('#00aa00')  
            edge_widths.append(2.0)
        else:
            edge_colors.append('gray')
            edge_widths.append(1.2)
    
    nx.draw(G, pos, ax=ax1, with_labels=True,
            node_color=node_colors, node_size=500,
            edge_color=edge_colors, width=edge_widths)
    
    labels = nx.get_edge_attributes(G, 'weight')
    nx.draw_networkx_edge_labels(G, pos, edge_labels=labels, ax=ax1, font_color='blue')
    
    ax1.set_title("Kruskal Visualization", fontsize=14)
    #ax1.text(0.02, -0.1, "Nhấn phím 'q' để kết thúc chương trình", fontsize=10, color="green", transform=ax1.transAxes)
    
    # ====================== PHẦN DƯỚI: PSEUDOCODE + LOG ======================
    ax2 = plt.subplot(2, 1, 2)
    ax2.axis('off')
    
    # ---------- POSITION ----------
    num_lines = len(PSEUDOCODE)
    line_spacing = 0.075
    top_y = 0.95                   
    code_bottom_y = top_y - (num_lines - 1) * line_spacing - 0.05  
    
    # ---------- FRAME ----------
    frame_x = 0.02
    frame_y = code_bottom_y - 0.05
    frame_width = 0.96
    frame_height = top_y - frame_y + 0.05
    
    rect = patches.Rectangle((frame_x, frame_y), frame_width, frame_height,
                             linewidth=2, edgecolor='black', facecolor='none')
    ax2.add_patch(rect)
    
    # ---------- PSEUDOCODE ---------
    for i, line in enumerate(PSEUDOCODE):
        color = "red" if i == code_line else "black"
        y_pos = top_y - i * line_spacing
        ax2.text(frame_x + 0.02, y_pos, line, fontsize=11, color=color,
                 va='top', fontfamily='monospace')
    
    # ---------- LOG ----------
    log_y = frame_y - 0.08  
    ax2.text(frame_x + 0.02, log_y, log_text, fontsize=11, color="blue",
             va="top", fontfamily='sans-serif', wrap=True)
    
    plt.tight_layout()
    plt.pause(2)
    
    # ====================== ADD(Edge) ======================
    if action == "add" and highlight_nodes:
        target_edge = next(e for e in current_mst if set([e.u, e.v]) == highlight_nodes)
        
        frames = 10
        for f in range(1, frames + 1):
            alpha = f / frames
            edge_colors_new = []
            edge_widths_new = []
            for u_v in G.edges:
                edge = Edge(u_v[0], u_v[1], G[u_v[0]][u_v[1]]['weight'])
                rev_edge = Edge(u_v[1], u_v[0], edge.w)
                if edge in current_mst or rev_edge in current_mst:
                    if {u_v[0], u_v[1]} == highlight_nodes:
                        r = alpha * 1.0
                        g = (1 - alpha) * 0.67
                        b = 0.0
                        edge_colors_new.append((r, g, b))
                        edge_widths_new.append(2.0)
                    else:
                        edge_colors_new.append('red')
                        edge_widths_new.append(2.0)
                else:
                    edge_colors_new.append('gray')
                    edge_widths_new.append(1.2)
            

            plt.clf()
            ax1 = plt.subplot(2, 1, 1)
            nx.draw(G, pos, ax=ax1, with_labels=True,
                    node_color=node_colors, node_size=500,
                    edge_color=edge_colors_new, width=edge_widths_new)
            nx.draw_networkx_edge_labels(G, pos, edge_labels=labels, ax=ax1)
            ax1.set_title("Kruskal Visualization", fontsize=14)
            
            ax2 = plt.subplot(2, 1, 2)
            ax2.axis('off')
            ax2.add_patch(patches.Rectangle((frame_x, frame_y), frame_width, frame_height,
                                            linewidth=2, edgecolor='black', facecolor='none'))
            for i, line in enumerate(PSEUDOCODE):
                color = "red" if i == code_line else "black"
                y_pos = top_y - i * line_spacing
                ax2.text(frame_x + 0.02, y_pos, line, fontsize=11, color=color,
                         va='top', fontfamily='monospace')
            ax2.text(frame_x + 0.02, log_y, log_text, fontsize=11, color="blue",
                     va="top", fontfamily='sans-serif')
            
            plt.tight_layout()
            plt.pause(pause * 0.08)
    
    plt.pause(1.5)