using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bjvvv_colapsediv.Visible = false;
            pBJVVV.Visible = false;
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
        MySqlDataAdapter da = new MySqlDataAdapter();
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
        gvBJVVV.DataSource = DS.Tables["t"];

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        LoadDataBJVVV();
        QueryTime = watch.Elapsed.TotalSeconds;
        lMSR.Text = "По запросу '"+TextBox1.Text+"' найдено "+SRCount+" документов за "+QueryTime+" сек";
        //pBJVVV.ScrollBars = ScrollBars.None;
    }
    protected void gvBJVVV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBJVVV.PageIndex = e.NewPageIndex;
        LoadDataBJVVV();
        gvBJVVV.DataBind();
    }



}
        //<!--ImageControlID="Image1"
        //ExpandedImage="~/ajax/images/collapse.jpg"
        //CollapsedImage="~/ajax/images/expand.jpg"-->
