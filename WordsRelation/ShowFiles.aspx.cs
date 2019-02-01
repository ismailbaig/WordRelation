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
                this.gvFiles.DataSource = context.MasterFiles
                    .Select(file => new { Name = file.Name }).ToList();
                this.gvFiles.DataBind();
            }

        }
    }
}