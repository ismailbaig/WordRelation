using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WordsRelation.Data;

namespace WordsRelation
{
    public partial class ShowFiles : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetFiles();
        }

        private void GetFiles()
        {
            using (ConceptsRelationDBEntities1 context = new ConceptsRelationDBEntities1())
            {
                List<MasterFile> msfiles = new List<MasterFile>();
                msfiles = context.MasterFiles.Select(file => file).ToList<MasterFile>();

                foreach(MasterFile m in msfiles)
                {
                    m.Name = m.Name.Split(new string[] { ".docx_" }, StringSplitOptions.None)[0].Replace("$sPaCe$", " ") + ".docx";
                }

                this.gvFiles.DataSource = msfiles;

                //this.gvFiles.DataSource = context.MasterFiles
                //    .Select( (file) => 
                //        new { Name = file.Name.Split(new string[] { ".docx_" }, StringSplitOptions.None)[0] }
                //        ).ToList();
                this.gvFiles.DataBind();
            }

        }
    }
}