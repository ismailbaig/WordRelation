using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WordsRelation.Data;

namespace WordsRelation
{
    public partial class DefaultPage : System.Web.UI.Page
    {

        static string DocxConvertedToHtmlDirectory = "DocxConvertedToHtml/";
        string fileNameFromUser = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SaveConceptsRelation_Click(object sender, EventArgs e)
        {

            using (ConceptsRelationDBEntities1 context = new ConceptsRelationDBEntities1())
            {
                context.ConceptsRelations.Add(new ConceptsRelation { ConceptOne = txtConcept1.Text, ConceptTwo = txtConcept2.Text,
                        RelationType = tags1.Text });
                

                context.SaveChanges();
            }


        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (FileUploadContent.HasFile)
            {
                try
                {
                    fileNameFromUser = string.Empty;
                    fileNameFromUser = FileUploadContent.FileName;

                    var fiFileName = new FileInfo(fileNameFromUser);
                    if (Util.IsWordprocessingML(fiFileName.Extension))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            memoryStream.Write(FileUploadContent.FileBytes, 0,
                                FileUploadContent.FileBytes.Length);

                            using (WordprocessingDocument wDoc =
                                WordprocessingDocument.Open(memoryStream, true))
                            {
                            }

                            lblMessage.Text = "File Uploaded Successfully";
                            Session["ByteArray"] = FileUploadContent.FileBytes;
                            Session["FileNameFromUser"] = fileNameFromUser;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Error: Not a WordprocessingML document";
                        Session["ByteArray"] = null;
                        Session["FileNameFromUser"] = null;

                    }

                }

                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message.ToString();
                    Session["ByteArray"] = null;
                    Session["FileNameFromUser"] = null;
                }
            }
            else
            {
                lblMessage.Text = "You have not specified a file.";
                Session["ByteArray"] = null;
                Session["FileNameFromUser"] = null;

            }

            byte[] byteArray = (byte[])(Session["ByteArray"]);

            if (byteArray != null)
            {
                try
                {
                    DirectoryInfo convertedDocsDirectory =
                        new DirectoryInfo(Server.MapPath(DocxConvertedToHtmlDirectory));

                    if (!convertedDocsDirectory.Exists)
                        convertedDocsDirectory.Create();

                    Guid g = Guid.NewGuid();
                    var htmlFileName = Session["FileNameFromUser"].ToString().Replace(".docx", "").Replace(" ", "$sPaC" +
                        "" +
                        "e$").ToString()
                        + "_" + g.ToString() + ".html";
                    ConvertToHtml(byteArray
                        , convertedDocsDirectory, htmlFileName);

                    //Store in Database the file name and path
                    using (ConceptsRelationDBEntities1 context = new ConceptsRelationDBEntities1())
                    {
                        context.MasterFiles.Add(new MasterFile
                        {
                            FilePath = convertedDocsDirectory.Name + @"\" + htmlFileName,
                            Name = htmlFileName
                        });

                        context.SaveChanges();
                    }

                    iframeDocViewer.Controls.Add(new LiteralControl("<iframe width='560' height='315' " +
                        "src=" + @"DocxConvertedToHtml\" + htmlFileName + "></iframe><br />"));

                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message.ToString();
                }
            }
            else
            {
                lblMessage.Text = "You have not specified a file.";
            }
        }

        private static void ConvertToHtml(byte[] byteArray, DirectoryInfo destDirectory,
            string htmlFileName)
        {
            FileInfo fiHtml = new FileInfo(Path.Combine(destDirectory.FullName, htmlFileName));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (WordprocessingDocument wDoc =
                        WordprocessingDocument.Open(memoryStream, true))
                {
                    var imageDirecotryFullName =
                        fiHtml.FullName.Substring(0, fiHtml.FullName.Length - fiHtml.Extension.Length) + "_files";
                    var imageDirectoryRelativeName =
                        fiHtml.Name.Substring(0, fiHtml.Name.Length - fiHtml.Extension.Length) + "_files";
                    int imageCounter = 0;
                    var pageTitle = (string)wDoc
                                    .CoreFilePropertiesPart
                                    .GetXDocument()
                                    .Descendants(DC.title)
                                    .FirstOrDefault();
                    HtmlConverterSettings settings = new HtmlConverterSettings()
                    {
                        PageTitle = pageTitle,
                        FabricateCssClasses = true,
                        CssClassPrefix = "pt-",
                        RestrictToSupportedLanguages = false,
                        RestrictToSupportedNumberingFormats = false,
                        ImageHandler = imageInfo =>
                        {
                            DirectoryInfo localDirInfo = new DirectoryInfo(imageDirecotryFullName);
                            if (!localDirInfo.Exists)
                                localDirInfo.Create();
                            ++imageCounter;
                            string extension = imageInfo.ContentType.Split('/')[1].ToLower();
                            ImageFormat imageFormat = null;
                            if (extension == "png")
                            {
                                extension = "gif";
                                imageFormat = ImageFormat.Gif;
                            }
                            else if (extension == "gif")
                                imageFormat = ImageFormat.Gif;
                            else if (extension == "bmp")
                                imageFormat = ImageFormat.Bmp;
                            else if (extension == "jpeg")
                                imageFormat = ImageFormat.Jpeg;
                            else if (extension == "tiff")
                            {
                                extension = "gif";
                                imageFormat = ImageFormat.Gif;
                            }
                            else if (extension == "x-wmf")
                            {
                                extension = "wmf";
                                imageFormat = ImageFormat.Wmf;
                            }

                            if (imageFormat == null)
                                return null;

                            FileInfo imageFileName = new FileInfo(imageDirecotryFullName + "/image" +
                                imageCounter.ToString() + "." + extension);
                            try
                            {
                                imageInfo.Bitmap.Save(imageFileName.FullName, imageFormat);
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                return null;
                            }

                            XElement img = new XElement(Xhtml.img,
                                new XAttribute(NoNamespace.src, imageDirectoryRelativeName + "/" + imageFileName.Name),
                                imageInfo.ImgStyleAttribute,
                                imageInfo.AltText != null ?
                                    new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);

                            return img;


                        }
                    };

                    // XElement html = HtmlConverter.ConvertToHtml(wDoc, settings);
                    XElement html = HtmlConverter.ConvertToHtml(wDoc, settings);


                    var body = html.Descendants(Xhtml.body).First();
                    //body.AddFirst(
                    //    new XElement(Xhtml.p,
                    //        new XElement(Xhtml.a,
                    //            new XAttribute("href", "WebForm1.aspx"), "Go back to upload page")));

                    var htmlString = html.ToString(SaveOptions.DisableFormatting);

                    File.WriteAllText(fiHtml.FullName, htmlString, Encoding.UTF8);

                }
            }
        }

        protected void ShowFiles_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowFiles.aspx");
        }
    }
}