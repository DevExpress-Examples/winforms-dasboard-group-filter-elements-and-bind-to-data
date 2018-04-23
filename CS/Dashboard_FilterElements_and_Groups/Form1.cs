using System;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;

namespace Dashboard_FilterElements_and_Groups {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            Dashboard dashboard1 = new Dashboard();

            DashboardSqlDataSource dataSource = new DashboardSqlDataSource();
            dataSource.ConnectionParameters =
                new XmlFileConnectionParameters(@"..\..\Data\WebsiteStatisticsData.xml");
            SelectQuery selectQuery = SelectQueryFluentBuilder
                .AddTable("Data")
                .SelectColumns("Browser", "BrowserDetails", "Date", "Count")
                .Build("Statistics");
            dataSource.Queries.Add(selectQuery);
            dashboard1.DataSources.Add(dataSource);

            ComboBoxDashboardItem comboBox1 = new ComboBoxDashboardItem();
            comboBox1.Name = "Browser";
            comboBox1.DataSource = dataSource; comboBox1.DataMember = "Statistics";
            comboBox1.FilterDimensions.Add(new Dimension("Browser"));
            comboBox1.ComboBoxType = ComboBoxDashboardItemType.Checked;

            ListBoxDashboardItem listBox1 = new ListBoxDashboardItem();
            listBox1.Name = "Browser Version";
            listBox1.DataSource = dataSource; listBox1.DataMember = "Statistics";
            listBox1.FilterDimensions.Add(new Dimension("BrowserDetails"));
            listBox1.InteractivityOptions.IgnoreMasterFilters = false;

            TreeViewDashboardItem treeView1 = new TreeViewDashboardItem();
            treeView1.Name = "Date";
            treeView1.DataSource = dataSource; treeView1.DataMember = "Statistics";
            treeView1.FilterDimensions.AddRange(new Dimension("Date", DateTimeGroupInterval.Year), 
                new Dimension("Date", DateTimeGroupInterval.Quarter));
            treeView1.AutoExpandNodes = true;

            DashboardItemGroup group1 = new DashboardItemGroup();
            group1.Name = "Filters";
            group1.InteractivityOptions.IsMasterFilter = true;
            dashboard1.Groups.Add(group1);
            group1.AddRange(comboBox1, treeView1, listBox1);

            ChartDashboardItem chart1 = new ChartDashboardItem();
            chart1.Name = "Browser Statistics";
            chart1.DataSource = dataSource; chart1.DataMember = "Statistics";
            chart1.Arguments.Add(new Dimension("Date", DateTimeGroupInterval.MonthYear));
            chart1.SeriesDimensions.AddRange(new Dimension("Browser"), new Dimension("BrowserDetails"));
            chart1.Panes.Add(new ChartPane());
            SimpleSeries salesAmountSeries = new SimpleSeries(SimpleSeriesType.SplineArea);
            salesAmountSeries.Value = new Measure("Count");
            chart1.Panes[0].Series.Add(salesAmountSeries);
            dashboard1.Items.Add(chart1);

            dashboardViewer1.Dashboard = dashboard1;            
        }

        private void Form1_Load(object sender, EventArgs e) {
            DashboardLayoutGroup layoutGroup1 = dashboardViewer1.
                Dashboard.LayoutRoot.FindRecursive(dashboardViewer1.Dashboard.Groups[0]);
            layoutGroup1.Orientation = DashboardLayoutGroupOrientation.Vertical;
            layoutGroup1.Weight = dashboardViewer1.Dashboard.LayoutRoot.Weight / 5;            
            dashboardViewer1.SetMasterFilter("comboBoxDashboardItem1", "Internet Explorer");
        }
    }
}
