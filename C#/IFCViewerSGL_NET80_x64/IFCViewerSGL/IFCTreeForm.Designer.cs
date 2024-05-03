namespace IFCViewerSGL
{
    partial class IFCTreeForm
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IFCTreeForm));
            this._treeView = new System.Windows.Forms.TreeView();
            this._ilIFCTree = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // _treeView
            // 
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.FullRowSelect = true;
            this._treeView.HideSelection = false;
            this._treeView.ImageIndex = 0;
            this._treeView.ImageList = this._ilIFCTree;
            this._treeView.Location = new System.Drawing.Point(0, 0);
            this._treeView.Name = "_treeView";
            this._treeView.SelectedImageIndex = 0;
            this._treeView.ShowNodeToolTips = true;
            this._treeView.Size = new System.Drawing.Size(333, 580);
            this._treeView.TabIndex = 0;
            this._treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this._treeView_BeforeExpand);
            this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeView_AfterSelect);
            this._treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._treeView_NodeMouseClick);
            // 
            // _ilIFCTree
            // 
            this._ilIFCTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_ilIFCTree.ImageStream")));
            this._ilIFCTree.TransparentColor = System.Drawing.Color.Transparent;
            this._ilIFCTree.Images.SetKeyName(0, "selectedAll.bmp");
            this._ilIFCTree.Images.SetKeyName(1, "selectedPart.bmp");
            this._ilIFCTree.Images.SetKeyName(2, "selectedNone.bmp");
            this._ilIFCTree.Images.SetKeyName(3, "propertySet.bmp");
            this._ilIFCTree.Images.SetKeyName(4, "property.bmp");
            this._ilIFCTree.Images.SetKeyName(5, "none.bmp");
            // 
            // IFCTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 580);
            this.Controls.Add(this._treeView);
            this.Location = new System.Drawing.Point(20, 100);
            this.Name = "IFCTreeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "IFC Tree";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IFCTreeForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.ImageList _ilIFCTree;
    }
}