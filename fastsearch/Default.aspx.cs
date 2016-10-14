using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using SolrNet;
using SolrNet.Attributes;
using Microsoft.Practices.ServiceLocation;
using SolrNet.Commands.Parameters;
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            bjvvv_colapsediv.Visible = false;
            
            pBJVVV.Visible = false;
            redkostj_colapsediv.Visible = false;
            pREDKOSTJ.Visible = false;
            bjacc_colapsediv.Visible = false;
            pBJACC.Visible = false;
            mardgani_colapsediv.Visible = false;
            pMARDGANI.Visible = false;
            bjfcc_colapsediv.Visible = false;
            pBJFCC.Visible = false;
            britsovet_colapsediv.Visible = false;
            pBRIT_SOVET.Visible = false;
            MainSearchResults.Visible = false;
        }
    }
    int SRCount = 0;
    double QueryTime = 0;
    Stopwatch watch = new Stopwatch();

   


    private void LoadDataBJVVV()
    {
        bjvvv_colapsediv.Visible = true;
        MainSearchResults.Visible = true;

        if (TextBox1.Text == "")
        {
            CollapsiblePanelExtender1.TextLabelID = "Ничего не найдено";
            return;
        }
        
        /*MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet DS = new DataSet();
        da.SelectCommand = new MySqlCommand();
        var cs = ConfigurationManager.ConnectionStrings["SphinxConnectionString"].ConnectionString;
        da.SelectCommand.Connection =  new MySqlConnection(cs);
        da.SelectCommand.Parameters.AddWithValue("searchstr", TextBox1.Text);
        da.SelectCommand.CommandText = "SELECT REMOVE_REPEATS((SELECT idmain,PLAIN FROM bjvvvidx where match(@searchstr) limit 0,10000), idmain, 0, 10000)";
        watch.Start();
        int i = da.Fill(DS, "pins");
        watch.Stop();
        SRCount += i;
        string pins = "(";
        foreach (DataRow r in DS.Tables["pins"].Rows)
        {
            pins += r["IDMAIN"].ToString() +",";
        }
        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";
        */
        //это получить из сфинкса.

        DataSet DS = new DataSet();

        //ISolrOperations<BJVVVRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<BJVVVRECORD>>();
        //SolrQueryResults<BJVVVRECORD> results = solr.Query(new SolrQuery("pushkin"));
        //foreach (BJVVVRECORD result in results)
        //{
        //    Response.Write(result.PLAIN);
        //}


        //ScriptManager.RegisterStartupScript(this, this.GetType(), "iiii", "alert( 1 );", true);

        ISolrOperations<BJVVVRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<BJVVVRECORD>>();
        var q = new QueryOptions();
        q.Rows = 500;
        SolrQueryResults<BJVVVRECORD> powerArticles = solr.Query(new SolrQuery(TextBox1.Text),q);
        string pins = "(";
        int i = powerArticles.Count;
        SRCount += i;
        foreach (BJVVVRECORD article in powerArticles)
        {
            //Console.WriteLine(string.Format("{0}: {1}", article.ID, article.PLAIN));
            //Response.Write(string.Format("{0}: {1}", article.ID, article.PLAIN));
            pins += article.IDMAIN + ",";

        }

        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";


        //Startup.Init<SOLRPOST.PostData.SolrSearchResult>("http://localhost:8983/solr");
        SqlDataAdapter msda = new SqlDataAdapter();
        msda.SelectCommand = new SqlCommand();
        var mscs = ConfigurationManager.ConnectionStrings["msConnectionString"].ConnectionString;
        msda.SelectCommand.Connection = new SqlConnection(mscs);
        
        msda.SelectCommand.CommandText = "select 'not implemented' as LEVEL,avt.PLAIN as AVT,tit.PLAIN as TIT,mesto.PLAIN as MESTO,pub.PLAIN as IZD,god.PLAIN as GOD,'not implemented' as ELIB "+
                                        ",A.IDMAIN IDMAIN   " +
                                        " from BJVVV..DATAEXT A  "+
                                        " left join BJVVV..DATAEXT Savt on A.IDMAIN = Savt.IDMAIN and Savt.MNFIELD = 700 and Savt.MSFIELD = '$a' " +
                                        " left join BJVVV..DATAEXTPLAIN avt on avt.IDDATAEXT = Savt.ID " +
                                        " left join BJVVV..DATAEXT Smesto on A.IDMAIN = Smesto.IDMAIN and Smesto.MNFIELD = 210 and Smesto.MSFIELD = '$a' " +
                                        " left join BJVVV..DATAEXTPLAIN mesto on mesto.IDDATAEXT = Smesto.ID " +
                                        " left join BJVVV..DATAEXT Spub on A.IDMAIN = Spub.IDMAIN and Spub.MNFIELD = 210 and Spub.MSFIELD = '$c' " +
                                        " left join BJVVV..DATAEXTPLAIN pub on pub.IDDATAEXT = Spub.ID " +
                                        " left join BJVVV..DATAEXT Sgod on A.IDMAIN = Sgod.IDMAIN and Sgod.MNFIELD = 2100 and Sgod.MSFIELD = '$d' " +
                                        " left join BJVVV..DATAEXTPLAIN god on god.IDDATAEXT = Sgod.ID " +
                                        " left join BJVVV..DATAEXTPLAIN tit on tit.IDDATAEXT = A.ID " +
                                        " where A.MNFIELD = 200 and A.MSFIELD = '$a' and A.IDMAIN in " + pins;
        int j = msda.Fill(DS, "t");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "iiii", "alert("+j+");", true);

        gvBJVVV.DataSource = DS.Tables["t"];
        ClientScript.RegisterStartupScript(this.GetType(), "alrt", "console.log(3);");
        ((BoundField)gvBJVVV.Columns[0]).DataField = "LEVEL";
        ((BoundField)gvBJVVV.Columns[1]).DataField = "AVT";
        ((BoundField)gvBJVVV.Columns[3]).DataField = "MESTO";
        ((BoundField)gvBJVVV.Columns[4]).DataField = "IZD";
        ((BoundField)gvBJVVV.Columns[5]).DataField = "GOD";
        ((BoundField)gvBJVVV.Columns[6]).DataField = "ELIB";

        //((TemplateField)gvBJVVV.Columns[2]).DataField = "TIT";

        gvBJVVV.DataBind();
        if (gvBJVVV.Rows.Count == 0)
        {
            lbBJVVV.Text = "Результаты поиска в базе основного фонда. Найдено: " + i.ToString() + ".";
            pBJVVV.Visible = false;
        }
        else
        {
            lbBJVVV.Text = "Результаты поиска в базе основного фонда. Найдено: " + i.ToString() + "...";
            pBJVVV.Visible = true;
        }
    }
    private void LoadDataREDKOSTJ()
    {
        redkostj_colapsediv.Visible = true;

        if (TextBox1.Text == "")
        {
            CollapsiblePanelExtender2.TextLabelID = "Ничего не найдено";
            pREDKOSTJ.Visible = false;
            return;
        }
        /*
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet DS = new DataSet();
        da.SelectCommand = new MySqlCommand();
        var cs = ConfigurationManager.ConnectionStrings["SphinxConnectionString"].ConnectionString;
        da.SelectCommand.Connection = new MySqlConnection(cs);
        da.SelectCommand.Parameters.AddWithValue("searchstr", TextBox1.Text);
        da.SelectCommand.CommandText = "SELECT REMOVE_REPEATS((SELECT idmain,PLAIN FROM redkostjidx where match(@searchstr) limit 0,10000), idmain, 0, 10000)";
        watch.Start();
        int i = da.Fill(DS, "pins");
        watch.Stop();
        SRCount += i;
        string pins = "(";
        foreach (DataRow r in DS.Tables["pins"].Rows)
        {
            pins += r["IDMAIN"].ToString() + ",";
        }
        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";*/
        DataSet DS = new DataSet();
        ISolrOperations<REDKOSTJRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<REDKOSTJRECORD>>();

        var q = new QueryOptions();
        q.Rows = 500;


        SolrQueryResults<REDKOSTJRECORD> powerArticles = solr.Query(new SolrQuery(TextBox1.Text),q);
        string pins = "(";
        int i = powerArticles.Count;
        SRCount += i;
        foreach (REDKOSTJRECORD article in powerArticles)
        {
            //Console.WriteLine(string.Format("{0}: {1}", article.ID, article.PLAIN));
            //Response.Write(string.Format("{0}: {1}", article.ID, article.PLAIN));
            pins += article.IDMAIN + ",";

        }
        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";

        SqlDataAdapter msda = new SqlDataAdapter();
        msda.SelectCommand = new SqlCommand();
        var mscs = ConfigurationManager.ConnectionStrings["msConnectionString"].ConnectionString;
        msda.SelectCommand.Connection = new SqlConnection(mscs);

        msda.SelectCommand.CommandText = "select 'not implemented' as LEVEL,avt.PLAIN as AVT,tit.PLAIN as TIT,mesto.PLAIN as MESTO,pub.PLAIN as IZD,god.PLAIN as GOD,'not implemented' as ELIB " +
                                        ",A.IDMAIN IDMAIN   " +
                                        " from REDKOSTJ..DATAEXT A  " +
                                        " left join REDKOSTJ..DATAEXT Savt on A.IDMAIN = Savt.IDMAIN and Savt.MNFIELD = 700 and Savt.MSFIELD = '$a' " +
                                        " left join REDKOSTJ..DATAEXTPLAIN avt on avt.IDDATAEXT = Savt.ID " +
                                        " left join REDKOSTJ..DATAEXT Smesto on A.IDMAIN = Smesto.IDMAIN and Smesto.MNFIELD = 210 and Smesto.MSFIELD = '$a' " +
                                        " left join REDKOSTJ..DATAEXTPLAIN mesto on mesto.IDDATAEXT = Smesto.ID " +
                                        " left join REDKOSTJ..DATAEXT Spub on A.IDMAIN = Spub.IDMAIN and Spub.MNFIELD = 210 and Spub.MSFIELD = '$c' " +
                                        " left join REDKOSTJ..DATAEXTPLAIN pub on pub.IDDATAEXT = Spub.ID " +
                                        " left join REDKOSTJ..DATAEXT Sgod on A.IDMAIN = Sgod.IDMAIN and Sgod.MNFIELD = 2100 and Sgod.MSFIELD = '$d' " +
                                        " left join REDKOSTJ..DATAEXTPLAIN god on god.IDDATAEXT = Sgod.ID " +
                                        " left join REDKOSTJ..DATAEXTPLAIN tit on tit.IDDATAEXT = A.ID " +
                                        " where A.MNFIELD = 200 and A.MSFIELD = '$a' and A.IDMAIN in " + pins;
        int j = msda.Fill(DS, "t");
        gvREDKOSTJ.DataSource = DS.Tables["t"];

        ((BoundField)gvREDKOSTJ.Columns[0]).DataField = "LEVEL";
        ((BoundField)gvREDKOSTJ.Columns[1]).DataField = "AVT";
        ((BoundField)gvREDKOSTJ.Columns[3]).DataField = "MESTO";
        ((BoundField)gvREDKOSTJ.Columns[4]).DataField = "IZD";
        ((BoundField)gvREDKOSTJ.Columns[5]).DataField = "GOD";
        ((BoundField)gvREDKOSTJ.Columns[6]).DataField = "ELIB";

        //((TemplateField)gvBJVVV.Columns[2]).DataField = "TIT";

        gvREDKOSTJ.DataBind();
        if (gvREDKOSTJ.Rows.Count == 0)
        {
            lbREDKOSTJ.Text = "Результаты поиска в базе редкой книги. Найдено: " + i.ToString() + ".";
            pREDKOSTJ.Visible = false;
        }
        else
        {
            lbREDKOSTJ.Text = "Результаты поиска в базе редкой книги. Найдено: " + i.ToString() + "...";
            pREDKOSTJ.Visible = true;
        }
    }
    private void LoadDataBJACC()
    {
        bjacc_colapsediv.Visible = true;

        if (TextBox1.Text == "")
        {
            CollapsiblePanelExtender3.TextLabelID = "Ничего не найдено";
            pBJACC.Visible = false;
            return;
        }
        /*MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet DS = new DataSet();
        da.SelectCommand = new MySqlCommand();
        var cs = ConfigurationManager.ConnectionStrings["SphinxConnectionString"].ConnectionString;
        da.SelectCommand.Connection = new MySqlConnection(cs);
        da.SelectCommand.Parameters.AddWithValue("searchstr", TextBox1.Text);
        da.SelectCommand.CommandText = "SELECT REMOVE_REPEATS((SELECT idmain,PLAIN FROM bjaccidx where match(@searchstr) limit 0,10000), idmain, 0, 10000)";
        watch.Start();
        int i = da.Fill(DS, "pins");
        watch.Stop();
        SRCount += i;
        string pins = "(";
        foreach (DataRow r in DS.Tables["pins"].Rows)
        {
            pins += r["IDMAIN"].ToString() + ",";
        }*/
        DataSet DS = new DataSet();
        ISolrOperations<BJACCRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<BJACCRECORD>>();
        var q = new QueryOptions();
        q.Rows = 500;
        


        SolrQueryResults<BJACCRECORD> powerArticles = solr.Query(new SolrQuery(TextBox1.Text),q);
        string pins = "(";
        int i = powerArticles.Count;
        SRCount += i;
        foreach (BJACCRECORD article in powerArticles)
        {
            //Console.WriteLine(string.Format("{0}: {1}", article.ID, article.PLAIN));
            //Response.Write(string.Format("{0}: {1}", article.ID, article.PLAIN));
            pins += article.IDMAIN + ",";

        }
        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";
        SqlDataAdapter msda = new SqlDataAdapter();
        msda.SelectCommand = new SqlCommand();
        var mscs = ConfigurationManager.ConnectionStrings["msConnectionString"].ConnectionString;
        msda.SelectCommand.Connection = new SqlConnection(mscs);

        msda.SelectCommand.CommandText = "select 'not implemented' as LEVEL,avt.PLAIN as AVT,tit.PLAIN as TIT,mesto.PLAIN as MESTO,pub.PLAIN as IZD,god.PLAIN as GOD,'not implemented' as ELIB " +
                                        ",A.IDMAIN IDMAIN   " +
                                        " from BJACC..DATAEXT A  " +
                                        " left join BJACC..DATAEXT Savt on A.IDMAIN = Savt.IDMAIN and Savt.MNFIELD = 700 and Savt.MSFIELD = '$a' " +
                                        " left join BJACC..DATAEXTPLAIN avt on avt.IDDATAEXT = Savt.ID " +
                                        " left join BJACC..DATAEXT Smesto on A.IDMAIN = Smesto.IDMAIN and Smesto.MNFIELD = 210 and Smesto.MSFIELD = '$a' " +
                                        " left join BJACC..DATAEXTPLAIN mesto on mesto.IDDATAEXT = Smesto.ID " +
                                        " left join BJACC..DATAEXT Spub on A.IDMAIN = Spub.IDMAIN and Spub.MNFIELD = 210 and Spub.MSFIELD = '$c' " +
                                        " left join BJACC..DATAEXTPLAIN pub on pub.IDDATAEXT = Spub.ID " +
                                        " left join BJACC..DATAEXT Sgod on A.IDMAIN = Sgod.IDMAIN and Sgod.MNFIELD = 2100 and Sgod.MSFIELD = '$d' " +
                                        " left join BJACC..DATAEXTPLAIN god on god.IDDATAEXT = Sgod.ID " +
                                        " left join BJACC..DATAEXTPLAIN tit on tit.IDDATAEXT = A.ID " +
                                        " where A.MNFIELD = 200 and A.MSFIELD = '$a' and A.IDMAIN in " + pins;
        int j = msda.Fill(DS, "t");
        gvBJACC.DataSource = DS.Tables["t"];

        ((BoundField)gvBJACC.Columns[0]).DataField = "LEVEL";
        ((BoundField)gvBJACC.Columns[1]).DataField = "AVT";
        ((BoundField)gvBJACC.Columns[3]).DataField = "MESTO";
        ((BoundField)gvBJACC.Columns[4]).DataField = "IZD";
        ((BoundField)gvBJACC.Columns[5]).DataField = "GOD";
        ((BoundField)gvBJACC.Columns[6]).DataField = "ELIB";

        //((TemplateField)gvBJVVV.Columns[2]).DataField = "TIT";

        gvBJACC.DataBind();
        if (gvBJACC.Rows.Count == 0)
        {
            lbBJACC.Text = "Результаты поиска в базе Центра Американской Культуры. Найдено: " + i.ToString() + ".";
            pBJACC.Visible = false;
        }
        else
        {
            lbBJACC.Text = "Результаты поиска в базе Центра Американской культуры. Найдено: " + i.ToString() + "...";
            pBJACC.Visible = true;
        }
    }
    private void LoadDataMARDGANI()
    {
        mardgani_colapsediv.Visible = true;

        if (TextBox1.Text == "")
        {
            CollapsiblePanelExtender4.TextLabelID = "Ничего не найдено";
            pMARDGANI.Visible = false;
            return;
        }
       /* MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet DS = new DataSet();
        da.SelectCommand = new MySqlCommand();
        var cs = ConfigurationManager.ConnectionStrings["SphinxConnectionString"].ConnectionString;
        da.SelectCommand.Connection = new MySqlConnection(cs);
        da.SelectCommand.Parameters.AddWithValue("searchstr", TextBox1.Text);
        da.SelectCommand.CommandText = "SELECT REMOVE_REPEATS((SELECT idmain,PLAIN FROM mardganiidx where match(@searchstr) limit 0,10000), idmain, 0, 10000)";
        watch.Start();
        int i = da.Fill(DS, "pins");
        watch.Stop();
        SRCount += i;
        string pins = "(";
        foreach (DataRow r in DS.Tables["pins"].Rows)
        {
            pins += r["IDMAIN"].ToString() + ",";
        }*/
        DataSet DS = new DataSet();
        ISolrOperations<MARDGANIRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<MARDGANIRECORD>>();

        var q = new QueryOptions();
        q.Rows = 500;


        SolrQueryResults<MARDGANIRECORD> powerArticles = solr.Query(new SolrQuery(TextBox1.Text),q);
        string pins = "(";
        int i = powerArticles.Count;
        SRCount += i;
        foreach (MARDGANIRECORD article in powerArticles)
        {
            //Console.WriteLine(string.Format("{0}: {1}", article.ID, article.PLAIN));
            //Response.Write(string.Format("{0}: {1}", article.ID, article.PLAIN));
            pins += article.IDMAIN + ",";

        }

        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";
        SqlDataAdapter msda = new SqlDataAdapter();
        msda.SelectCommand = new SqlCommand();
        var mscs = ConfigurationManager.ConnectionStrings["msConnectionString"].ConnectionString;
        msda.SelectCommand.Connection = new SqlConnection(mscs);

        msda.SelectCommand.CommandText = "select 'not implemented' as LEVEL,avt.PLAIN as AVT,tit.PLAIN as TIT,mesto.PLAIN as MESTO,pub.PLAIN as IZD,god.PLAIN as GOD,'not implemented' as ELIB " +
                                        ",A.IDMAIN IDMAIN   " +
                                        " from MARDGANI..DATAEXT A  " +
                                        " left join MARDGANI..DATAEXT Savt on A.IDMAIN = Savt.IDMAIN and Savt.MNFIELD = 700 and Savt.MSFIELD = '$a' " +
                                        " left join MARDGANI..DATAEXTPLAIN avt on avt.IDDATAEXT = Savt.ID " +
                                        " left join MARDGANI..DATAEXT Smesto on A.IDMAIN = Smesto.IDMAIN and Smesto.MNFIELD = 210 and Smesto.MSFIELD = '$a' " +
                                        " left join MARDGANI..DATAEXTPLAIN mesto on mesto.IDDATAEXT = Smesto.ID " +
                                        " left join MARDGANI..DATAEXT Spub on A.IDMAIN = Spub.IDMAIN and Spub.MNFIELD = 210 and Spub.MSFIELD = '$c' " +
                                        " left join MARDGANI..DATAEXTPLAIN pub on pub.IDDATAEXT = Spub.ID " +
                                        " left join MARDGANI..DATAEXT Sgod on A.IDMAIN = Sgod.IDMAIN and Sgod.MNFIELD = 2100 and Sgod.MSFIELD = '$d' " +
                                        " left join MARDGANI..DATAEXTPLAIN god on god.IDDATAEXT = Sgod.ID " +
                                        " left join MARDGANI..DATAEXTPLAIN tit on tit.IDDATAEXT = A.ID " +
                                        " where A.MNFIELD = 200 and A.MSFIELD = '$a' and A.IDMAIN in " + pins;
        int j = msda.Fill(DS, "t");
        gvMARDGANI.DataSource = DS.Tables["t"];

        ((BoundField)gvMARDGANI.Columns[0]).DataField = "LEVEL";
        ((BoundField)gvMARDGANI.Columns[1]).DataField = "AVT";
        ((BoundField)gvMARDGANI.Columns[3]).DataField = "MESTO";
        ((BoundField)gvMARDGANI.Columns[4]).DataField = "IZD";
        ((BoundField)gvMARDGANI.Columns[5]).DataField = "GOD";
        ((BoundField)gvMARDGANI.Columns[6]).DataField = "ELIB";

        //((TemplateField)gvBJVVV.Columns[2]).DataField = "TIT";

        gvMARDGANI.DataBind();
        if (gvMARDGANI.Rows.Count == 0)
        {
            lbMARDGANI.Text = "Результаты поиска в базе фонда Марджани. Найдено: " + i.ToString() + ".";
            pMARDGANI.Visible = false;
        }
        else
        {
            lbMARDGANI.Text = "Результаты поиска в базе фонда Марджани. Найдено: " + i.ToString() + "...";
            pMARDGANI.Visible = true;
        }
    }
    private void LoadDataBJFCC()
    {
        bjfcc_colapsediv.Visible = true;

        if (TextBox1.Text == "")
        {
            CollapsiblePanelExtender5.TextLabelID = "Ничего не найдено";
            pBJFCC.Visible = false;
            return;
        }

        DataSet DS = new DataSet();
        ISolrOperations<BJFCCRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<BJFCCRECORD>>();

        var q = new QueryOptions();
        q.Rows = 500;


        SolrQueryResults<BJFCCRECORD> powerArticles = solr.Query(new SolrQuery(TextBox1.Text), q);
        string pins = "(";
        int i = powerArticles.Count;
        SRCount += i;
        foreach (BJFCCRECORD article in powerArticles)
        {
            //Console.WriteLine(string.Format("{0}: {1}", article.ID, article.PLAIN));
            //Response.Write(string.Format("{0}: {1}", article.ID, article.PLAIN));
            pins += article.IDMAIN + ",";

        }

        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";
        SqlDataAdapter msda = new SqlDataAdapter();
        msda.SelectCommand = new SqlCommand();
        var mscs = ConfigurationManager.ConnectionStrings["msConnectionString"].ConnectionString;
        msda.SelectCommand.Connection = new SqlConnection(mscs);

        msda.SelectCommand.CommandText = "select 'not implemented' as LEVEL,avt.PLAIN as AVT,tit.PLAIN as TIT,mesto.PLAIN as MESTO,pub.PLAIN as IZD,god.PLAIN as GOD,'not implemented' as ELIB " +
                                        ",A.IDMAIN IDMAIN   " +
                                        " from BJFCC..DATAEXT A  " +
                                        " left join BJFCC..DATAEXT Savt on A.IDMAIN = Savt.IDMAIN and Savt.MNFIELD = 700 and Savt.MSFIELD = '$a' " +
                                        " left join BJFCC..DATAEXTPLAIN avt on avt.IDDATAEXT = Savt.ID " +
                                        " left join BJFCC..DATAEXT Smesto on A.IDMAIN = Smesto.IDMAIN and Smesto.MNFIELD = 210 and Smesto.MSFIELD = '$a' " +
                                        " left join BJFCC..DATAEXTPLAIN mesto on mesto.IDDATAEXT = Smesto.ID " +
                                        " left join BJFCC..DATAEXT Spub on A.IDMAIN = Spub.IDMAIN and Spub.MNFIELD = 210 and Spub.MSFIELD = '$c' " +
                                        " left join BJFCC..DATAEXTPLAIN pub on pub.IDDATAEXT = Spub.ID " +
                                        " left join BJFCC..DATAEXT Sgod on A.IDMAIN = Sgod.IDMAIN and Sgod.MNFIELD = 2100 and Sgod.MSFIELD = '$d' " +
                                        " left join BJFCC..DATAEXTPLAIN god on god.IDDATAEXT = Sgod.ID " +
                                        " left join BJFCC..DATAEXTPLAIN tit on tit.IDDATAEXT = A.ID " +
                                        " where A.MNFIELD = 200 and A.MSFIELD = '$a' and A.IDMAIN in " + pins;
        int j = msda.Fill(DS, "t");
        gvBJFCC.DataSource = DS.Tables["t"];

        ((BoundField)gvBJFCC.Columns[0]).DataField = "LEVEL";
        ((BoundField)gvBJFCC.Columns[1]).DataField = "AVT";
        ((BoundField)gvBJFCC.Columns[3]).DataField = "MESTO";
        ((BoundField)gvBJFCC.Columns[4]).DataField = "IZD";
        ((BoundField)gvBJFCC.Columns[5]).DataField = "GOD";
        ((BoundField)gvBJFCC.Columns[6]).DataField = "ELIB";

        //((TemplateField)gvBJVVV.Columns[2]).DataField = "TIT";

        gvBJFCC.DataBind();
        if (gvBJFCC.Rows.Count == 0)
        {
            lbBJFCC.Text = "Результаты поиска в базе Французского Культурного Центра. Найдено: " + i.ToString() + ".";
            pBRIT_SOVET.Visible = false;
        }
        else
        {
            lbBJFCC.Text = "Результаты поиска в базе Французского Культурного Центра. Найдено: " + i.ToString() + "...";
            pBJFCC.Visible = true;
        }
    }
    private void LoadDataBRIT_SOVET()
    {
        britsovet_colapsediv.Visible = true;

        if (TextBox1.Text == "")
        {
            CollapsiblePanelExtender6.TextLabelID = "Ничего не найдено";
            pBRIT_SOVET.Visible = false;
            return;
        }

        DataSet DS = new DataSet();
        ISolrOperations<BRIT_SOVETRECORD> solr = ServiceLocator.Current.GetInstance<ISolrOperations<BRIT_SOVETRECORD>>();

        var q = new QueryOptions();
        q.Rows = 500;


        SolrQueryResults<BRIT_SOVETRECORD> powerArticles = solr.Query(new SolrQuery(TextBox1.Text), q);
        string pins = "(";
        int i = powerArticles.Count;
        SRCount += i;
        foreach (BRIT_SOVETRECORD article in powerArticles)
        {
            //Console.WriteLine(string.Format("{0}: {1}", article.ID, article.PLAIN));
            //Response.Write(string.Format("{0}: {1}", article.ID, article.PLAIN));
            pins += article.IDMAIN + ",";

        }

        pins = pins.Remove(pins.Length - 1) + ")";
        if (pins == ")") pins = "(null)";
        SqlDataAdapter msda = new SqlDataAdapter();
        msda.SelectCommand = new SqlCommand();
        var mscs = ConfigurationManager.ConnectionStrings["msConnectionString"].ConnectionString;
        msda.SelectCommand.Connection = new SqlConnection(mscs);

        msda.SelectCommand.CommandText = "select 'not implemented' as LEVEL,avt.PLAIN as AVT,tit.PLAIN as TIT,mesto.PLAIN as MESTO,pub.PLAIN as IZD,god.PLAIN as GOD,'not implemented' as ELIB " +
                                        ",A.IDMAIN IDMAIN   " +
                                        " from BRIT_SOVET..DATAEXT A  " +
                                        " left join BRIT_SOVET..DATAEXT Savt on A.IDMAIN = Savt.IDMAIN and Savt.MNFIELD = 700 and Savt.MSFIELD = '$a' " +
                                        " left join BRIT_SOVET..DATAEXTPLAIN avt on avt.IDDATAEXT = Savt.ID " +
                                        " left join BRIT_SOVET..DATAEXT Smesto on A.IDMAIN = Smesto.IDMAIN and Smesto.MNFIELD = 210 and Smesto.MSFIELD = '$a' " +
                                        " left join BRIT_SOVET..DATAEXTPLAIN mesto on mesto.IDDATAEXT = Smesto.ID " +
                                        " left join BRIT_SOVET..DATAEXT Spub on A.IDMAIN = Spub.IDMAIN and Spub.MNFIELD = 210 and Spub.MSFIELD = '$c' " +
                                        " left join BRIT_SOVET..DATAEXTPLAIN pub on pub.IDDATAEXT = Spub.ID " +
                                        " left join BRIT_SOVET..DATAEXT Sgod on A.IDMAIN = Sgod.IDMAIN and Sgod.MNFIELD = 2100 and Sgod.MSFIELD = '$d' " +
                                        " left join BRIT_SOVET..DATAEXTPLAIN god on god.IDDATAEXT = Sgod.ID " +
                                        " left join BRIT_SOVET..DATAEXTPLAIN tit on tit.IDDATAEXT = A.ID " +
                                        " where A.MNFIELD = 200 and A.MSFIELD = '$a' and A.IDMAIN in " + pins;
        int j = msda.Fill(DS, "t");
        gvBRIT_SOVET.DataSource = DS.Tables["t"];

        ((BoundField)gvBRIT_SOVET.Columns[0]).DataField = "LEVEL";
        ((BoundField)gvBRIT_SOVET.Columns[1]).DataField = "AVT";
        ((BoundField)gvBRIT_SOVET.Columns[3]).DataField = "MESTO";
        ((BoundField)gvBRIT_SOVET.Columns[4]).DataField = "IZD";
        ((BoundField)gvBRIT_SOVET.Columns[5]).DataField = "GOD";
        ((BoundField)gvBRIT_SOVET.Columns[6]).DataField = "ELIB";

        //((TemplateField)gvBJVVV.Columns[2]).DataField = "TIT";

        gvBRIT_SOVET.DataBind();
        if (gvBRIT_SOVET.Rows.Count == 0)
        {
            lbBRIT_SOVET.Text = "Результаты поиска в базе фонда Британского Совета. Найдено: " + i.ToString() + ".";
            pBRIT_SOVET.Visible = false;
        }
        else
        {
            lbBRIT_SOVET.Text = "Результаты поиска в базе фонда Британского Совета. Найдено: " + i.ToString() + "...";
            pBRIT_SOVET.Visible = true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        LoadDataBJVVV();
        LoadDataREDKOSTJ();
        LoadDataBJACC();
        LoadDataMARDGANI();
        LoadDataBJFCC();
        LoadDataBRIT_SOVET();
        QueryTime = watch.Elapsed.TotalSeconds;
        lMSR.Text = "По запросу '"+TextBox1.Text+"' найдено "+SRCount+" документов за "+QueryTime+" сек";
    }
    protected void gvBJVVV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBJVVV.PageIndex = e.NewPageIndex;
        LoadDataBJVVV();
        gvBJVVV.DataBind();
    }



    protected void gvREDKOSTJ_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvREDKOSTJ.PageIndex = e.NewPageIndex;
        LoadDataREDKOSTJ();
        gvREDKOSTJ.DataBind();

    }
    protected void gvBJACC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBJACC.PageIndex = e.NewPageIndex;
        LoadDataBJACC();
        gvBJACC.DataBind();

    }

    protected void gvMARDGANI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMARDGANI.PageIndex = e.NewPageIndex;
        LoadDataMARDGANI();
        gvMARDGANI.DataBind();

    }


    protected void gvBJFCC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBJFCC.PageIndex = e.NewPageIndex;
        LoadDataBJFCC();
        gvBRIT_SOVET.DataBind();

    }


    protected void gvBRIT_SOVET_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBRIT_SOVET.PageIndex = e.NewPageIndex;
        LoadDataBRIT_SOVET();
        gvBRIT_SOVET.DataBind();

    }

}
 
        //<!--ImageControlID="Image1"
        //ExpandedImage="~/ajax/images/collapse.jpg"
        //CollapsedImage="~/ajax/images/expand.jpg"-->
