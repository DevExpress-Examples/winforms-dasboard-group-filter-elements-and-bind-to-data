Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraEditors
Imports System

Namespace Dashboard_FilterElements_and_Groups
	Partial Public Class Form1
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			Dim dataSource As DashboardSqlDataSource = CreateDataSource()
			dashboardViewer1.Dashboard = CreateDashboard(dataSource)
			dashboardViewer1.SetMasterFilter("comboBoxDashboardItem1", "Internet Explorer")
		End Sub

		Private Shared Function CreateDataSource() As DashboardSqlDataSource
			Dim dataSource As New DashboardSqlDataSource()
			dataSource.ConnectionParameters = New XmlFileConnectionParameters("..\..\Data\WebsiteStatisticsData.xml")
			Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable("Data").SelectColumns("Browser", "BrowserDetails", "Date", "Count").Build("Statistics")
			dataSource.Queries.Add(selectQuery)
			Return dataSource
		End Function
		Private Shared Function CreateDashboard(ByVal dataSource As DashboardSqlDataSource) As Dashboard
			Dim dashboard1 As New Dashboard()
			dashboard1.DataSources.Add(dataSource)

			Dim comboBox1 As New ComboBoxDashboardItem() With {.Name = "Browser", .DataSource = dataSource, .DataMember = "Statistics", .ComboBoxType = ComboBoxDashboardItemType.Checked}
			comboBox1.FilterDimensions.Add(New Dimension("Browser"))

			Dim listBox1 As New ListBoxDashboardItem() With {.Name = "Browser Version", .DataSource = dataSource, .DataMember = "Statistics"}
			listBox1.FilterDimensions.Add(New Dimension("BrowserDetails"))
			listBox1.InteractivityOptions.IgnoreMasterFilters = False

			Dim treeView1 As New TreeViewDashboardItem() With {.Name = "Date", .DataSource = dataSource, .DataMember = "Statistics", .AutoExpandNodes = True}
			treeView1.FilterDimensions.AddRange(New Dimension("Date", DateTimeGroupInterval.Year), New Dimension("Date", DateTimeGroupInterval.Quarter))

			Dim group1 As New DashboardItemGroup() With {.Name = "Filters"}
			group1.InteractivityOptions.IsMasterFilter = True
			dashboard1.Groups.Add(group1)
			group1.AddRange(comboBox1, treeView1, listBox1)

			Dim chart1 As New ChartDashboardItem() With {.Name = "Browser Statistics", .DataSource = dataSource, .DataMember = "Statistics"}
			chart1.Arguments.Add(New Dimension("Date", DateTimeGroupInterval.MonthYear))
			chart1.SeriesDimensions.AddRange(New Dimension("Browser"), New Dimension("BrowserDetails"))
			chart1.Panes.Add(New ChartPane())
			Dim salesAmountSeries As New SimpleSeries(SimpleSeriesType.SplineArea)
			salesAmountSeries.Value = New Measure("Count")
			chart1.Panes(0).Series.Add(salesAmountSeries)
			dashboard1.Items.Add(chart1)

			Return dashboard1
		End Function
	End Class
End Namespace
