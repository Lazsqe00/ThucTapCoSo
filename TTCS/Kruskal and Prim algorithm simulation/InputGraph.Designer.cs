using System.Text.Json;

namespace Kruskal_and_Prim_algorithm_simulation
{
    partial class InputGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void ShowEdgeList()
        {
            grpEdgeList.Visible = true;
            dgvEdges.Visible = true;
            btnGenerateEdges.Visible = true;
            btnManualInputEdges.Visible = true;

            grpAdjMatrix.Visible = false;
            dgvMatrix.Visible = false;
            btnGenerateMatrix.Visible = false;
            btnManualInputMatrix.Visible = false;
        }
        private void ShowAdjMatrix()
        {
            grpAdjMatrix.Visible = true;
            dgvMatrix.Visible = true;
            btnGenerateMatrix.Visible = true;
            btnManualInputMatrix.Visible = true;

            grpEdgeList.Visible = false;
            dgvEdges.Visible = false;
            btnGenerateEdges.Visible = false;
            btnManualInputEdges.Visible = false;
        }


        private void GenerateEdgeRows()
        {
            dgvEdges.BackgroundColor = Color.White;
            dgvEdges.DefaultCellStyle.BackColor = Color.White;
            dgvEdges.EnableHeadersVisualStyles = false;
            dgvEdges.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvEdges.RowHeadersVisible = false;
            dgvEdges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvEdges.Columns.Clear();
            dgvEdges.Rows.Clear();

            dgvEdges.Columns.Add("From", "Từ");
            dgvEdges.Columns.Add("To", "Đến");
            dgvEdges.Columns.Add("Weight", "Trọng số");

            for (int i = 1; i <= numEdges.Value - 1; i++)
            {
                dgvEdges.Rows.Add("", "", "");
            }
        }

        private void GenerateAdjMatrix()
        {
            int n = (int)numVertices.Value;
            dgvMatrix.BackgroundColor = Color.White;
            dgvMatrix.DefaultCellStyle.BackColor = Color.White;
            dgvMatrix.EnableHeadersVisualStyles = false;
            dgvMatrix.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvMatrix.RowHeadersVisible = false;
            dgvMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- Reset ---
            dgvMatrix.Columns.Clear();
            dgvMatrix.Rows.Clear();

            for (int i = 1; i <= n; i++)
            {
                dgvMatrix.Columns.Add("C" + i, i.ToString());
            }

            for (int r = 0; r < n; r++)
            {
                object[] rowValues = new object[n];

                for (int c = 0; c < n; c++)
                    rowValues[c] = 0;

                dgvMatrix.Rows.Add(rowValues);
            }
        }


        private void UpdateInputMode()
        {
            bool isEdgeList = radEdgeList.Checked;

            grpEdgeList.Visible = isEdgeList;
            dgvEdges.Visible = isEdgeList;
            btnGenerateEdges.Visible = isEdgeList;
            btnManualInputEdges.Visible = isEdgeList;

            grpAdjMatrix.Visible = !isEdgeList;
            dgvMatrix.Visible = !isEdgeList;
            btnGenerateMatrix.Visible = !isEdgeList;
            btnManualInputMatrix.Visible = !isEdgeList;

            if (isEdgeList)
                GenerateEdgeRows();
            else
                GenerateAdjMatrix();
        }

        // check so canh
        private bool ValidateEdges()
        {
            int n = (int)numVertices.Value;
            int e = (int)numEdges.Value;

            int maxEdges = n * (n - 1) / 2;
            int minEdges = 1;

            if (e < minEdges || e > maxEdges)
            {
                MessageBox.Show(
                    $"Số cạnh không hợp lệ!\n\n" +
                    $"Với {n} đỉnh, số cạnh hợp lệ nằm trong khoảng [1, {maxEdges}].",
                    "Lỗi đầu vào",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }

        // generate
        private void GenerateRandomEdges()
        {
            int n = (int)numVertices.Value; 
            int e = (int)numEdges.Value;    

            dgvEdges.Rows.Clear();

            Random rand = new Random();
            HashSet<string> used = new HashSet<string>();

            for (int i = 1; i <= e; i++)
            {
                int u, v;
                string key;
                do
                {
                    u = rand.Next(0, n);
                    v = rand.Next(0, n);

                    if (u > v) (u, v) = (v, u); 

                    key = $"{u}-{v}";
                }
                while (u == v || used.Contains(key));

                used.Add(key);

                int weight = rand.Next(1, 10); 

                dgvEdges.Rows.Add(u, v, weight);
            }
        }

        private void GenerateRandomAdjMatrix()
        {
            int n = (int)numVertices.Value;

            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();

            dgvMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatrix.RowHeadersVisible = false;
            dgvMatrix.AllowUserToAddRows = false;

            for (int i = 1; i <= n; i++)
                dgvMatrix.Columns.Add("C" + i, i.ToString());

            for (int i = 0; i < n; i++)
            {
                object[] row = new object[n];
                for (int j = 0; j < n; j++)
                    row[j] = 0;

                dgvMatrix.Rows.Add(row);
            }

            Random rand = new Random();
            int e = (int)numEdges.Value;

            HashSet<string> used = new HashSet<string>();

            for (int k = 0; k < e; k++)
            {
                int u, v;
                string key;

                do
                {
                    u = rand.Next(0, n);
                    v = rand.Next(0, n);

                    if (u > v)
                        (u, v) = (v, u);

                    key = $"{u}-{v}";
                }
                while (u == v || used.Contains(key));

                used.Add(key);

                int weight = rand.Next(1, 10);

                dgvMatrix.Rows[u].Cells[v].Value = weight;
                dgvMatrix.Rows[v].Cells[u].Value = weight;
            }

            dgvMatrix.Refresh();
        }

        private void ExportGraphToJson()
        {
            int n = (int)numVertices.Value; 
            List<int> nodes = new List<int>();
            for (int i = 0; i < n; i++)
                nodes.Add(i); 

            List<object> edges = new List<object>();
            if (radEdgeList.Checked) 
            {
                foreach (DataGridViewRow row in dgvEdges.Rows)
                {
                    if (row.IsNewRow) continue; 
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null)
                        continue; 

                    int u = int.Parse(row.Cells[0].Value.ToString());
                    int v = int.Parse(row.Cells[1].Value.ToString());
                    int w = int.Parse(row.Cells[2].Value.ToString());
                    edges.Add(new { u, v, w });
                }
            }
            else 
            {             
                int size = dgvMatrix.Rows.Count;
                for (int i = 0; i < size; i++)
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        if (dgvMatrix.Rows[i].Cells[j].Value == null) continue;

                        int w = int.Parse(dgvMatrix.Rows[i].Cells[j].Value.ToString());
                        if (w > 0)
                            edges.Add(new { u = i, v = j, w });
                    }
                }
            }

            //var graph = new { nodes, edges };
            //string json = JsonSerializer.Serialize(graph, new JsonSerializerOptions { WriteIndented = true });

            //string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            //string filePath = Path.Combine(projectDirectory, "graph.json");

            //File.WriteAllText(filePath, json);

            var graph = new { nodes, edges }; 
            string json = JsonSerializer.Serialize(graph, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("graph.json", json);
        }


        private void GenerateRandomEdgesConnected()
        {
            int n = (int)numVertices.Value;
            int e = (int)numEdges.Value;

            int maxEdges = n * (n - 1) / 2;
            if (e < n - 1)
            {
                MessageBox.Show($"Đồ thị phải có ít nhất {n - 1} cạnh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }       
        

            dgvEdges.Rows.Clear();

            Random rand = new Random();
            HashSet<string> used = new HashSet<string>();

 
            List<int> vertices = Enumerable.Range(0, n).ToList();
            for (int i = vertices.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                (vertices[i], vertices[j]) = (vertices[j], vertices[i]);
            }


            for (int i = 1; i < n; i++)
            {
                int u = vertices[i];
                int v = vertices[rand.Next(0, i)]; 

                int a = Math.Min(u, v);
                int b = Math.Max(u, v);
                string key = $"{a}-{b}";

                used.Add(key);

                int weight = rand.Next(1, 10);
                dgvEdges.Rows.Add(a, b, weight);
            }

            int currentEdges = n - 1;
            while (currentEdges < e)
            {
                int u = rand.Next(0, n);
                int v = rand.Next(0, n);
                if (u == v) continue;

                int a = Math.Min(u, v);
                int b = Math.Max(u, v);
                string key = $"{a}-{b}";

                if (used.Contains(key)) continue;

                used.Add(key);

                int weight = rand.Next(1, 10);
                dgvEdges.Rows.Add(a, b, weight);
                currentEdges++;
            }
        }

        private void GenerateRandomAdjMatrixConnected()
        {
            int n = (int)numVertices.Value;
            int e = (int)numEdges.Value;

            int maxEdges = n * (n - 1) / 2;

            if (e < n - 1)
            {
                MessageBox.Show($"Đồ thị phải có ít nhất {n - 1} cạnh.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvMatrix.Rows.Clear();
            dgvMatrix.Columns.Clear();

            dgvMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatrix.RowHeadersVisible = false;
            dgvMatrix.AllowUserToAddRows = false;

            for (int i = 1; i <= n; i++)
                dgvMatrix.Columns.Add("C" + i, i.ToString());

            for (int i = 0; i < n; i++)
            {
                object[] row = new object[n];
                for (int j = 0; j < n; j++)
                    row[j] = 0;
                dgvMatrix.Rows.Add(row);
            }

            Random rand = new Random();
            HashSet<string> used = new HashSet<string>();

            List<int> vertices = Enumerable.Range(0, n).ToList();
            for (int i = vertices.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                (vertices[i], vertices[j]) = (vertices[j], vertices[i]);
            }

            for (int i = 1; i < n; i++)
            {
                int u = vertices[i];
                int v = vertices[rand.Next(0, i)];

                int a = Math.Min(u, v);
                int b = Math.Max(u, v);
                string key = $"{a}-{b}";

                used.Add(key);

                int weight = rand.Next(1, 10);
                dgvMatrix.Rows[a].Cells[b].Value = weight;
                dgvMatrix.Rows[b].Cells[a].Value = weight;
            }

            int currentEdges = n - 1;
            while (currentEdges < e)
            {
                int u = rand.Next(0, n);
                int v = rand.Next(0, n);
                if (u == v) continue;

                int a = Math.Min(u, v);
                int b = Math.Max(u, v);
                string key = $"{a}-{b}";

                if (used.Contains(key)) continue;

                used.Add(key);

                int weight = rand.Next(1, 10);
                dgvMatrix.Rows[a].Cells[b].Value = weight;
                dgvMatrix.Rows[b].Cells[a].Value = weight;
                currentEdges++;
            }

            dgvMatrix.Refresh();
        }


        private void DFS(int u, List<int>[] adj, bool[] visited)
        {
            visited[u] = true;
            foreach (int v in adj[u])
            {
                if (!visited[v])
                    DFS(v, adj, visited);
            }
        }

        private bool CheckConnectedFromEdges()
        {
            int n = (int)numVertices.Value;
            
            List<int>[] adj = new List<int>[n];
            for (int i = 0; i < n; i++)
                adj[i] = new List<int>();

            foreach (DataGridViewRow row in dgvEdges.Rows)
            {
                if (row.IsNewRow) continue;

                if (!int.TryParse(Convert.ToString(row.Cells[0].Value), out int u)) continue;
                if (!int.TryParse(Convert.ToString(row.Cells[1].Value), out int v)) continue;

                if (u < 0 || u >= n || v < 0 || v >= n) continue;
                if (u == v) continue;

                adj[u].Add(v);
                adj[v].Add(u);
            }

            bool[] visited = new bool[n];
            DFS(0, adj, visited);

            for (int i = 0; i < n; i++)
                if (!visited[i]) return false;

            return true;
        }

   
        private bool CheckConnectedFromMatrix()
        {
            int n = (int)numVertices.Value;

            List<int>[] adj = new List<int>[n];
            for (int i = 0; i < n; i++)
                adj[i] = new List<int>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    object val = dgvMatrix.Rows[i].Cells[j].Value;
                    if (val == null) continue;
                    if (!int.TryParse(val.ToString(), out int w)) continue;
                    if (w > 0) adj[i].Add(j);
                }
            }

            bool[] visited = new bool[n];
            DFS(0, adj, visited);

            for (int i = 0; i < n; i++)
                if (!visited[i]) return false;

            return true;
        }

    //=======================End============================
        private void InitializeComponent()
        {
            grpGraphConfig = new GroupBox();
            label2 = new Label();
            numEdges = new NumericUpDown();
            label3 = new Label();
            btnReset = new Button();
            btnExecute = new Button();
            radAdjMatrix = new RadioButton();
            radEdgeList = new RadioButton();
            label1 = new Label();
            numVertices = new NumericUpDown();
            labelSoDinh = new Label();
            grpAdjMatrix = new GroupBox();
            btnConnectMatrix = new Button();
            btnManualInputMatrix = new Button();
            btnGenerateMatrix = new Button();
            dgvMatrix = new DataGridView();
            btnConfirm = new Button();
            grpInput = new GroupBox();
            grpEdgeList = new GroupBox();
            btnConnectEdges = new Button();
            btnManualInputEdges = new Button();
            btnGenerateEdges = new Button();
            dgvEdges = new DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            grpGraphConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEdges).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numVertices).BeginInit();
            grpAdjMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMatrix).BeginInit();
            grpInput.SuspendLayout();
            grpEdgeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEdges).BeginInit();
            SuspendLayout();
            // 
            // grpGraphConfig
            // 
            grpGraphConfig.Controls.Add(label2);
            grpGraphConfig.Controls.Add(numEdges);
            grpGraphConfig.Controls.Add(label3);
            grpGraphConfig.Controls.Add(btnReset);
            grpGraphConfig.Controls.Add(btnExecute);
            grpGraphConfig.Controls.Add(radAdjMatrix);
            grpGraphConfig.Controls.Add(radEdgeList);
            grpGraphConfig.Controls.Add(label1);
            grpGraphConfig.Controls.Add(numVertices);
            grpGraphConfig.Controls.Add(labelSoDinh);
            grpGraphConfig.Location = new Point(28, 12);
            grpGraphConfig.Name = "grpGraphConfig";
            grpGraphConfig.Size = new Size(824, 243);
            grpGraphConfig.TabIndex = 0;
            grpGraphConfig.TabStop = false;
            grpGraphConfig.Text = "Thông số đồ thị";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(377, 96);
            label2.Name = "label2";
            label2.Size = new Size(139, 20);
            label2.TabIndex = 11;
            label2.Text = "(số cạnh của đồ thị)";
            label2.Click += label2_Click;
            // 
            // numEdges
            // 
            numEdges.Location = new Point(205, 94);
            numEdges.Maximum = new decimal(new int[] { 1225, 0, 0, 0 });
            numEdges.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEdges.Name = "numEdges";
            numEdges.Size = new Size(150, 27);
            numEdges.TabIndex = 10;
            numEdges.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(111, 96);
            label3.Name = "label3";
            label3.Size = new Size(64, 20);
            label3.TabIndex = 9;
            label3.Text = "Số cạnh:";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(426, 191);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(126, 37);
            btnReset.TabIndex = 8;
            btnReset.Text = "Thiết lập lại";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            btnExecute.Location = new Point(270, 191);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(126, 37);
            btnExecute.TabIndex = 7;
            btnExecute.Text = "Thực hiện";
            btnExecute.UseVisualStyleBackColor = true;
            btnExecute.Click += btnExecute_Click;
            // 
            // radAdjMatrix
            // 
            radAdjMatrix.AutoSize = true;
            radAdjMatrix.Location = new Point(347, 143);
            radAdjMatrix.Name = "radAdjMatrix";
            radAdjMatrix.Size = new Size(100, 24);
            radAdjMatrix.TabIndex = 6;
            radAdjMatrix.Text = "Ma trận kề";
            radAdjMatrix.UseVisualStyleBackColor = true;
            radAdjMatrix.CheckedChanged += radAdjMatrix_CheckedChanged;
            // 
            // radEdgeList
            // 
            radEdgeList.AutoSize = true;
            radEdgeList.Checked = true;
            radEdgeList.Location = new Point(186, 143);
            radEdgeList.Name = "radEdgeList";
            radEdgeList.Size = new Size(133, 24);
            radEdgeList.TabIndex = 5;
            radEdgeList.TabStop = true;
            radEdgeList.Text = "Danh sách cạnh";
            radEdgeList.UseVisualStyleBackColor = true;
            radEdgeList.CheckedChanged += radEdgeList_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(377, 50);
            label1.Name = "label1";
            label1.Size = new Size(150, 20);
            label1.TabIndex = 2;
            label1.Text = "(đánh số từ 2 đến 50)";
            // 
            // numVertices
            // 
            numVertices.Location = new Point(205, 48);
            numVertices.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numVertices.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numVertices.Name = "numVertices";
            numVertices.Size = new Size(150, 27);
            numVertices.TabIndex = 1;
            numVertices.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // labelSoDinh
            // 
            labelSoDinh.AutoSize = true;
            labelSoDinh.Location = new Point(111, 48);
            labelSoDinh.Name = "labelSoDinh";
            labelSoDinh.Size = new Size(62, 20);
            labelSoDinh.TabIndex = 0;
            labelSoDinh.Text = "Số đỉnh:";
            // 
            // grpAdjMatrix
            // 
            grpAdjMatrix.Controls.Add(btnConnectMatrix);
            grpAdjMatrix.Controls.Add(btnManualInputMatrix);
            grpAdjMatrix.Controls.Add(btnGenerateMatrix);
            grpAdjMatrix.Controls.Add(dgvMatrix);
            grpAdjMatrix.Location = new Point(35, 46);
            grpAdjMatrix.Name = "grpAdjMatrix";
            grpAdjMatrix.Size = new Size(779, 301);
            grpAdjMatrix.TabIndex = 2;
            grpAdjMatrix.TabStop = false;
            grpAdjMatrix.Text = "Ma trận kề";
            // 
            // btnConnectMatrix
            // 
            btnConnectMatrix.Location = new Point(576, 229);
            btnConnectMatrix.Name = "btnConnectMatrix";
            btnConnectMatrix.Size = new Size(126, 37);
            btnConnectMatrix.TabIndex = 4;
            btnConnectMatrix.Text = "Tạo liên thông";
            btnConnectMatrix.UseVisualStyleBackColor = true;
            btnConnectMatrix.Click += btnConnectMatrix_Click;
            // 
            // btnManualInputMatrix
            // 
            btnManualInputMatrix.Location = new Point(420, 229);
            btnManualInputMatrix.Name = "btnManualInputMatrix";
            btnManualInputMatrix.Size = new Size(126, 37);
            btnManualInputMatrix.TabIndex = 3;
            btnManualInputMatrix.Text = "Nhập tay";
            btnManualInputMatrix.UseVisualStyleBackColor = true;
            btnManualInputMatrix.Click += btnManualInputMatrix_Click;
            // 
            // btnGenerateMatrix
            // 
            btnGenerateMatrix.Location = new Point(276, 229);
            btnGenerateMatrix.Name = "btnGenerateMatrix";
            btnGenerateMatrix.Size = new Size(126, 37);
            btnGenerateMatrix.TabIndex = 1;
            btnGenerateMatrix.Text = "Tạo ngẫu nhiên";
            btnGenerateMatrix.UseVisualStyleBackColor = true;
            btnGenerateMatrix.Click += btnGenerateMatrix_Click;
            // 
            // dgvMatrix
            // 
            dgvMatrix.AllowUserToAddRows = false;
            dgvMatrix.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMatrix.Dock = DockStyle.Top;
            dgvMatrix.Location = new Point(3, 23);
            dgvMatrix.Name = "dgvMatrix";
            dgvMatrix.RowHeadersVisible = false;
            dgvMatrix.RowHeadersWidth = 51;
            dgvMatrix.Size = new Size(773, 179);
            dgvMatrix.TabIndex = 0;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(570, 362);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(161, 37);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Xác nhận thông tin";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // grpInput
            // 
            grpInput.Controls.Add(btnConfirm);
            grpInput.Controls.Add(grpAdjMatrix);
            grpInput.Controls.Add(grpEdgeList);
            grpInput.Location = new Point(28, 269);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(824, 416);
            grpInput.TabIndex = 1;
            grpInput.TabStop = false;
            grpInput.Text = "Nhập dữ liệu đồ thị";
            // 
            // grpEdgeList
            // 
            grpEdgeList.Controls.Add(btnConnectEdges);
            grpEdgeList.Controls.Add(btnManualInputEdges);
            grpEdgeList.Controls.Add(btnGenerateEdges);
            grpEdgeList.Controls.Add(dgvEdges);
            grpEdgeList.Location = new Point(35, 46);
            grpEdgeList.Name = "grpEdgeList";
            grpEdgeList.Size = new Size(738, 301);
            grpEdgeList.TabIndex = 0;
            grpEdgeList.TabStop = false;
            grpEdgeList.Text = "Danh sách cạnh";
            // 
            // btnConnectEdges
            // 
            btnConnectEdges.Location = new Point(541, 243);
            btnConnectEdges.Name = "btnConnectEdges";
            btnConnectEdges.Size = new Size(126, 37);
            btnConnectEdges.TabIndex = 5;
            btnConnectEdges.Text = "Tạo liên thông";
            btnConnectEdges.UseVisualStyleBackColor = true;
            btnConnectEdges.Click += btnConnectEdges_Click;
            // 
            // btnManualInputEdges
            // 
            btnManualInputEdges.Location = new Point(408, 243);
            btnManualInputEdges.Name = "btnManualInputEdges";
            btnManualInputEdges.Size = new Size(114, 37);
            btnManualInputEdges.TabIndex = 4;
            btnManualInputEdges.Text = "Nhập tay";
            btnManualInputEdges.UseVisualStyleBackColor = true;
            btnManualInputEdges.Click += btnManualInputEdges_Click;
            // 
            // btnGenerateEdges
            // 
            btnGenerateEdges.Location = new Point(252, 243);
            btnGenerateEdges.Name = "btnGenerateEdges";
            btnGenerateEdges.Size = new Size(129, 37);
            btnGenerateEdges.TabIndex = 3;
            btnGenerateEdges.Text = "Tạo ngẫu nhiên";
            btnGenerateEdges.UseVisualStyleBackColor = true;
            btnGenerateEdges.Click += btnGenerateEdges_Click;
            // 
            // dgvEdges
            // 
            dgvEdges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEdges.Dock = DockStyle.Top;
            dgvEdges.Location = new Point(3, 23);
            dgvEdges.Name = "dgvEdges";
            dgvEdges.RowHeadersWidth = 51;
            dgvEdges.Size = new Size(732, 200);
            dgvEdges.TabIndex = 0;
            // 
            // InputGraph
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(975, 697);
            Controls.Add(grpInput);
            Controls.Add(grpGraphConfig);
            Name = "InputGraph";
            Text = "InputGraph";
            Load += InputGraph_Load;
            grpGraphConfig.ResumeLayout(false);
            grpGraphConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numEdges).EndInit();
            ((System.ComponentModel.ISupportInitialize)numVertices).EndInit();
            grpAdjMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMatrix).EndInit();
            grpInput.ResumeLayout(false);
            grpEdgeList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEdges).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpGraphConfig;
        private NumericUpDown numVertices;
        private Label labelSoDinh;
        private Label label1;
        private RadioButton radAdjMatrix;
        private RadioButton radEdgeList;
        private GroupBox grpInput;
        private GroupBox grpEdgeList;
        private DataGridView dgvEdges;
        private GroupBox grpAdjMatrix;
        private Button btnGenerateMatrix;
        private DataGridView dgvMatrix;
        private Button btnManualInputMatrix;
        private Button btnGenerateEdges;
        private Button btnManualInputEdges;
        private Button btnReset;
        private Button btnExecute;
        private NumericUpDown numEdges;
        private Label label3;
        private Label label2;
        private Button btnConfirm;
        private Button btnConnectMatrix;
        private Button btnConnectEdges;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}