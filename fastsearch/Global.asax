<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения
        //SolrNet.Startup.Init<BJVVVRECORD>("http://127.0.0.1:8983/solr/bjvvv");
        //SolrNet.Startup.Init<REDKOSTJRECORD>("http://127.0.0.1:8984/solr/redkostj");
        //SolrNet.Startup.Init<MARDGANIRECORD>("http://127.0.0.1:8985/solr/mardgani");
        //SolrNet.Startup.Init<BJACCRECORD>("http://127.0.0.1:8986/solr/bjacc");

        SolrNet.Startup.Init<BJVVVRECORD>("http://80.250.173.142:8983/solr/bjvvv");
        SolrNet.Startup.Init<REDKOSTJRECORD>("http://80.250.173.142:8984/solr/redkostj");
        SolrNet.Startup.Init<MARDGANIRECORD>("http://80.250.173.142:8985/solr/mardgani");
        SolrNet.Startup.Init<BJACCRECORD>("http://80.250.173.142:8986/solr/bjacc");
        SolrNet.Startup.Init<BJFCCRECORD>("http://80.250.173.142:8987/solr/bjfcc");
        SolrNet.Startup.Init<BRIT_SOVETRECORD>("http://80.250.173.142:8988/solr/britsovet");
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Код, выполняемый при завершении работы приложения

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Код, выполняемый при возникновении необрабатываемой ошибки

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске нового сеанса

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Код, выполняемый при запуске приложения. 
        // Примечание. Событие Session_End вызывается только в том случае, если для режима sessionstate
        // задано значение InProc в файле Web.config. Если для режима сеанса задано значение StateServer 
        // или SQLServer, событие не порождается.

    }
       
</script>
