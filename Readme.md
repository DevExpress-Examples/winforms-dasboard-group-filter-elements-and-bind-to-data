<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128581035/18.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T191632)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Dashboard for WinForms - How to Group Filter Elements and Bind to Data at Runtime

This example demonstrates how to combine [filter elements](https://docs.devexpress.com/Dashboard/17659) in a [group](https://docs.devexpress.com/Dashboard/1758) and [bind them to data](https://docs.devexpress.com/Dashboard/17660) in code.

## Files to Review

* [Form1.cs](./CS/Dashboard_FilterElements_and_Groups/Form1.cs) (VB: [Form1.vb](./VB/Dashboard_FilterElements_and_Groups/Form1.vb))

## Example Overview

The dashboard contains the following filter items combined in a group:

* [ComboBoxDashboardItem](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.ComboBoxDashboardItem)
* [ListBoxDashboardItem](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.ListBoxDashboardItem)
* [TreeViewDashboardItem](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.TreeViewDashboardItem)

The group serve as a master filter for the [Chart dashboard item](https://docs.devexpress.com/Dashboard/14719).

![screenshot](/images/screenshot.png)

## Documentation

- [Binding to Data](https://docs.devexpress.com/Dashboard/116771) 
- [Dashboard Items](https://docs.devexpress.com/Dashboard/116521)
- [Dashboard Layout](https://docs.devexpress.com/Dashboard/15617)
- [WinForms Viewer](https://docs.devexpress.com/Dashboard/117122)

## More Examples

- [How to bind a Range Filter dashboard item to data in code](https://github.com/DevExpress-Examples/how-to-bind-a-range-filter-dashboard-item-to-data-in-code-e4773)
- [How to bind a Date Filter dashboard item to data in code](https://github.com/DevExpress-Examples/winforms-dashboard-create-datefilterdashboarditem)
