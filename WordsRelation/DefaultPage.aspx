<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="DefaultPage.aspx.cs" Inherits="WordsRelation.DefaultPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap.min.css" rel="stylesheet">
	<link href="css/font-awesome.min.css" rel="stylesheet">
	<link href="css/datepicker3.css" rel="stylesheet">
	<link href="css/styles.css" rel="stylesheet">
	
	<!--Custom Font-->
	<link href="https://fonts.googleapis.com/css?family=Montserrat:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">
	



    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
		<div class="row">
			<ol class="breadcrumb">
				<li><a href="#">
					<em class="fa fa-home"></em>
				</a></li>
				<li class="active">New Relation</li>
			</ol>
		</div><!--/.row-->
		
		<div class="row">
			<div class="col-lg-12">
				<h1 class="page-header">Make New Relation</h1>
			</div>
		</div><!--/.row-->
				
		
		<div class="row">
			<div class="col-lg-12">
			
				
				
				<div class="panel panel-default">
				
					<div class="panel-body">
						<div class="col-md-8">
							<form role="form">
								
			
								<div class="form-group">
									<label>File input</label>
									<%--<input type="file">--%>
                                    <asp:FileUpload ID="FileUploadContent" runat="server" />
									
                                    <br />
                                    <p>
                                     <asp:Button class="btn btn-primary" ID="btnUploadFile" runat="server"
                                         Text="Upload" OnClick="btnUploadFile_Click"/>
                                    &nbsp;&nbsp;
                                     <asp:Button class="btn btn-primary" ID="ShowFiles" runat="server"
                                         Text="ShowFiles" OnClick="ShowFiles_Click"/>
                                    </p>
                                    <p>
                                        <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
                                    </p>
								</div>
								<div class="form-group">
									<%--<label>Text area</label>
									<textarea class="form-control" rows="3"></textarea>--%>
                                    <label>File View</label>
                                    <div>
                                     <asp:PlaceHolder ID="iframeDocViewer" runat="server"/>
                                    </div>
                                    <p>
                                        &nbsp;</p>
								</div>
						
								</div>
								<div class="col-md-4">
							<div class="form-group">
									<label>Concept 1</label>
									<%--<input class="form-control" placeholder="Placeholder">--%>
                                <asp:TextBox ID="txtConcept1" class="form-control" runat="server"></asp:TextBox>
								</div>
								<div class="form-group">
									<label>Concept 2</label>
									<%--<input type="password" class="form-control">--%>
                                     <asp:TextBox ID="txtConcept2" class="form-control" runat="server"></asp:TextBox>
								</div>
									<div class="form-group">
										<label>Relation Type</label>
                                         <asp:TextBox ID="tags1" runat="server" class="form-control" value="" maxlength="20" style="width:50%"></asp:TextBox>
									
                                    ion 4</option>
										</select>--%>
									</div>
							
									<asp:Button class="btn btn-primary" ID="SaveConceptsRelation" runat="server" Text="Save" OnClick="SaveConceptsRelation_Click"/>
								</div>
							</form>
						</div>
					</div>
				</div>
			</div>

        <div class="row">
            
        </div>
		</div>





    <script src="js/jquery-1.11.1.min.js"></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/chart.min.js"></script>
	<script src="js/chart-data.js"></script>
	<script src="js/easypiechart.js"></script>
	<script src="js/easypiechart-data.js"></script>
	<script src="js/bootstrap-datepicker.js"></script>
	<script src="js/custom.js"></script>
	<script>
		window.onload = function () {
	var chart1 = document.getElementById("line-chart").getContext("2d");
	window.myLine = new Chart(chart1).Line(lineChartData, {
	responsive: true,
	scaleLineColor: "rgba(0,0,0,.2)",
	scaleGridLineColor: "rgba(0,0,0,.05)",
	scaleFontColor: "#c5c7cc"
	});
};
	</script>




    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <script>
  $( function() {
    var availableTags = [
      "ActionScript",
      "AppleScript",
      "Asp",
      "BASIC",
      "C",
      "C++",
      "Clojure",
      "COBOL",
      "ColdFusion",
      "Erlang",
      "Fortran",
      "Groovy",
      "Haskell",
      "Java",
      "JavaScript",
      "Lisp",
      "Perl",
      "PHP",
      "Python",
      "Ruby",
      "Scala",
      "Scheme"
    ];
    $( "#tags" ).autocomplete({
      source: availableTags
    });
  } );
  </script>
</asp:Content>
