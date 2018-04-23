Imports System
Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors

Namespace Dashboard_FilterElements_and_Groups
    Partial Public Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            Dim dashboard1 As New Dashboard()

            Dim dataSource As New DashboardSqlDataSource()
            dataSource.ConnectionParameters =
                New XmlFileConnectionParameters("..\..\Data\WebsiteStatisticsData.xml")
            Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable("Data") _
                                             .SelectColumns("Browser", "BrowserDetails", "Date", "Count") _
                                             .Build("Statistics")
            dataSource.Queries.Add(selectQuery)
            dashboard1.DataSources.Add(dataSource)

            Dim comboBox1 As New ComboBoxDashboardItem()
            comboBox1.Name = "Browser"
            comboBox1.DataSource = dataSource
            comboBox1.DataMember = "Statistics"
            comboBox1.FilterDimensions.Add(New Dimension("Browser"))
            comboBox1.ComboBoxType = ComboBoxDashboardItemType.Checked

            Dim listBox1 As New ListBoxDashboardItem()
            listBox1.Name = "Browser Version"
            listBox1.DataSource = dataSource
            listBox1.DataMember = "Statistics"
            listBox1.FilterDimensions.Add(New Dimension("BrowserDetails"))
            listBox1.InteractivityOptions.IgnoreMasterFilters = False

            Dim treeView1 As New TreeViewDashboardItem()
            treeView1.Name = "Date"
            treeView1.DataSource = dataSource
            treeView1.DataMember = "Statistics"
            treeView1.FilterDimensions.AddRange(New Dimension("Date", DateTimeGroupInterval.Year), _
                                                New Dimension("Date", DateTimeGroupInterval.Quarter))
            treeView1.AutoExpandNodes = True

            Dim group1 As New DashboardItemGroup()
            group1.Name = "Filters"
            group1.InteractivityOptions.IsMasterFilter = True
            dashboard1.Groups.Add(group1)
            group1.AddRange(comboBox1, treeView1, listBox1)

            Dim chart1 As New ChartDashboardItem()
            chart1.Name = "Browser Statistics"
            chart1.DataSource = dataSource
            chart1.DataMember = "Statistics"
            chart1.Arguments.Add(New Dimension("Date", DateTimeGroupInterval.MonthYear))
            chart1.SeriesDimensions.AddRange(New Dimension("Browser"), New Dimension("BrowserDetails"))
            chart1.Panes.Add(New ChartPane())
            Dim salesAmountSeries As New SimpleSeries(SimpleSeriesType.SplineArea)
            salesAmountSeries.Value = New Measure("Count")
            chart1.Panes(0).Series.Add(salesAmountSeries)
            dashboard1.Items.Add(chart1)

            dashboardViewer1.Dashboard = dashboard1
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim layoutGroup1 As DashboardLayoutGroup =
                dashboardViewer1.Dashboard.LayoutRoot.FindRecursive(dashboardViewer1.Dashboard.Groups(0))
            layoutGroup1.Orientation = DashboardLayoutGroupOrientation.Vertical
            layoutGroup1.Weight = dashboardViewer1.Dashboard.LayoutRoot.Weight / 5
            dashboardViewer1.SetMasterFilter("comboBoxDashboardItem1", "Internet Explorer")
        End Sub
    End Class
End Namespace
