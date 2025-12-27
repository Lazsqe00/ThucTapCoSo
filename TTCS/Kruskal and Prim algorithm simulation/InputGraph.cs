using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kruskal_and_Prim_algorithm_simulation
{
    public partial class InputGraph : Form
    {
        public InputGraph()
        {
            InitializeComponent();
        }
        private void InputGraph_Load(object sender, EventArgs e)
        {
            radEdgeList.Checked = true;
            UpdateInputMode();
        }

        private void radEdgeList_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radAdjMatrix_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (!ValidateEdges()) return;
            // fix loi tu them canh
            dgvEdges.AllowUserToAddRows = true;
            if (radEdgeList.Checked)
            {
                ShowEdgeList();
                GenerateEdgeRows();
            }
            else
            {
                ShowAdjMatrix();
                GenerateAdjMatrix();
            }
        }

        private void btnGenerateMatrix_Click(object sender, EventArgs e)
        {
            GenerateRandomAdjMatrix();
        }

        private void btnGenerateEdges_Click(object sender, EventArgs e)
        {
            if (!ValidateEdges()) return;
            dgvEdges.AllowUserToAddRows = false;
            GenerateRandomEdges();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnManualInputMatrix_Click(object sender, EventArgs e)
        {
            ShowAdjMatrix();
            GenerateAdjMatrix();
        }

        private void btnManualInputEdges_Click(object sender, EventArgs e)
        {
            dgvEdges.AllowUserToAddRows = true;
            ShowEdgeList();
            GenerateEdgeRows();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ExportGraphToJson();
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string edit_py = Path.Combine(projectDirectory, "input_graph", "edit_positions.py");
            var result = MessageBox.Show(
                "Bạn muốn mô phỏng thuật toán nào?\nYes: Prim\nNo: Kruskal",
                "Chọn thuật toán",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("Bạn đã hủy lựa chọn.");
                return;
            }

            string scriptPath;
            if (result == DialogResult.Yes)
            {
                bool isConnected = radEdgeList.Checked ? CheckConnectedFromEdges() : CheckConnectedFromMatrix();
                if(!isConnected)
                { 
                    MessageBox.Show("Đồ thị phải đảm bảo tính liên thông.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                scriptPath = Path.Combine(projectDirectory, "Prim", "main.py");
            }
            else
            {
                scriptPath = Path.Combine(projectDirectory, "Kruskal", "main.py");
            }

            try
            {
                var pse = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{edit_py}\"",
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardError = true, //bat loi
                    WindowStyle = ProcessWindowStyle.Normal
                };

                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardError = true, //bat loi
                    WindowStyle = ProcessWindowStyle.Normal
                };
                var procEdit = System.Diagnostics.Process.Start(pse);
                procEdit.WaitForExit();
                var procPrim = System.Diagnostics.Process.Start(psi);
                procPrim.WaitForExit();

                string graphJS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "graph.json");
                string posJS = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "positions.json");

                //delete
                if (File.Exists(graphJS)) File.Delete(graphJS);

                if (File.Exists(posJS)) File.Delete(posJS);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chạy Python: " + ex.Message);
            }
        }

        private void btnConnectMatrix_Click(object sender, EventArgs e)
        {
            GenerateRandomAdjMatrixConnected();
        }

        private void btnConnectEdges_Click(object sender, EventArgs e)
        {
            GenerateRandomEdgesConnected();
        }
    }
}
