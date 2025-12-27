import matplotlib.pyplot as plt
import networkx as nx
import matplotlib.patches as patches

PSEUDOCODE = [
    "1.  MST := empty",
    "2.  visited := {s}",
    "3.  PQ := all edges from s (by weight)",
    "4.  while PQ not empty:",
    "5.      (u, v, w) = PQ.pop_min()",
    "6.      if v in visited: continue",
    "7.      MST.add((u,v))",
    "8.      visited.add(v)",
    "9.      push all edges (v,x) with x not visited into PQ",
    "10. return MST"
]

def draw_step(G, pos, step, n, pause=1.5):
    plt.clf()
    action, current_mst, code_line, highlight_nodes, visited_snapshot, log_text = step

    ax1 = plt.subplot(2, 1, 1)

    visited_nodes = set(visited_snapshot)

    # ===== NODE COLORS =====
    node_colors = []
    for node in G.nodes:
        if node in visited_nodes:
            node_colors.append("#ff8c00")     
        elif highlight_nodes and node in highlight_nodes:
            node_colors.append("gray")       
        else:
            node_colors.append("lightgray")  

    # ===== EDGE COLORS + WIDTHS =====
    edge_colors = []
    edge_widths = []

    for u, v in G.edges:
        in_mst = any(
            (u == e.u and v == e.v) or (u == e.v and v == e.u)
            for e in current_mst
        )

        if in_mst:
            edge_colors.append("red")   
            edge_widths.append(2.0)    
        else:
            edge_colors.append("gray")  
            edge_widths.append(1.2)     

    nx.draw(
        G, pos, ax=ax1, with_labels=True,
        node_color=node_colors,
        node_size=500,
        edge_color=edge_colors,
        width=edge_widths
    )

    labels = nx.get_edge_attributes(G, 'weight')
    nx.draw_networkx_edge_labels(G, pos, edge_labels=labels, ax=ax1)

    ax1.set_title("Prim Visualization")

    # ===== PSEUDOCODE PANEL =====
    ax2 = plt.subplot(2, 1, 2)
    ax2.axis('off')

    rect = patches.Rectangle(
        (0, 0.15), 1, 0.85,
        linewidth=2, edgecolor='black', facecolor='none'
    )
    ax2.add_patch(rect)

    for i, line in enumerate(PSEUDOCODE):
        color = "red" if i == code_line else "black"
        ax2.text(0.02, 0.9 - i * 0.075, line, fontsize=11, color=color)

    ax2.text(0.02, 0.05, log_text, fontsize=11, color="blue")

    plt.tight_layout()
    plt.pause(2)

    # ===== ANIMATION: VISIT NODE =====
    if action == "visit" and highlight_nodes:
        v = next(iter(highlight_nodes))

        frames = 6
        for f in range(1, frames + 1):
            alpha = f / frames
            node_colors_anim = []

            for node in G.nodes:
                if node in visited_nodes:
                    node_colors_anim.append("#ff8c00")
                elif node == v:
                    r = (1 - alpha) * 0.5 + alpha * 1.0
                    g = (1 - alpha) * 0.5 + alpha * 0.549
                    b = (1 - alpha) * 0.5 + alpha * 0.0
                    node_colors_anim.append((r, g, b, 1.0))
                else:
                    node_colors_anim.append("lightgray")

            plt.clf()
            ax1 = plt.subplot(2, 1, 1)
            nx.draw(
                G, pos, ax=ax1, with_labels=True,
                node_color=node_colors_anim,
                node_size=500,
                edge_color=edge_colors,
                width=edge_widths
            )
            nx.draw_networkx_edge_labels(G, pos, edge_labels=labels, ax=ax1)
            ax1.set_title("Prim Visualization")

            ax2 = plt.subplot(2, 1, 2)
            ax2.axis('off')
            ax2.add_patch(
                patches.Rectangle((0, 0.15), 1, 0.85,
                                  linewidth=2, edgecolor='black', facecolor='none')
            )

            for i, line in enumerate(PSEUDOCODE):
                color = "red" if i == code_line else "black"
                ax2.text(0.02, 0.9 - i * 0.075, line, fontsize=11, color=color)

            ax2.text(0.02, 0.05, log_text, fontsize=11, color="blue")

            plt.tight_layout()
            plt.pause(pause * 0.05)

        visited_nodes.add(v)
        plt.pause(3)
