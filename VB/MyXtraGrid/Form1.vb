﻿' Developer Express Code Central Example:
' How to customize the GridControl's print output.
' 
' This example demonstrates how to override the default exporting process to take
' into account a custom drawn content provided via the
' GridView.CustomDrawFooterCell Event
' (ms-help://DevExpress.NETv10.1/DevExpress.WindowsForms/DevExpressXtraGridViewsGridGridView_CustomDrawFooterCelltopic.htm)
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2667

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils

Namespace MyXtraGrid
	Partial Public Class Form1
		Inherits Form

		Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(Date))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i Mod 3), i, 3 - i, Date.Now.AddDays(i) })
			Next i
			Return tbl
		End Function


		Public Sub New()
			InitializeComponent()
			myGridControl1.DataSource = CreateTable(20)
		End Sub

		Private Sub myGridView1_CustomDrawFooterCell(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs) Handles myGridView1.CustomDrawFooterCell
			Dim isPrinting As Boolean = e.Handled
			e.Handled = True
			e.Appearance.BackColor = Color.Yellow
			e.Appearance.FillRectangle(e.Cache, e.Bounds)
			Dim rect As Rectangle = e.Bounds
			rect.Width = rect.Height
			e.Appearance.FillRectangle(e.Cache, rect)
			e.Cache.DrawLine(New Pen(Brushes.Red, 3), New Point(rect.X, rect.Y), New Point(rect.Right, rect.Bottom))
			e.Cache.DrawEllipse(rect.X, rect.Y, rect.Width, rect.Height, Color.Red, 3)
			e.Cache.Paint.DrawString(e.Cache, "Custom Draw", AppearanceObject.DefaultFont, Brushes.Black, e.Bounds.Location)
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			myGridControl1.ShowPrintPreview()
		End Sub

		Private Sub myGridView1_CustomDrawRowIndicator(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs) Handles myGridView1.CustomDrawRowIndicator

			Dim rect As Rectangle = e.Info.Bounds
			rect.Width = 50
			e.Info.Bounds = rect
			e.Cache.FillRectangle(Brushes.Green, e.Bounds)
			e.Cache.Paint.DrawString(e.Cache, "Hello, friend!!!", Me.Font, Brushes.Red, e.Bounds, e.Appearance.GetStringFormat())
			e.Handled = True
		End Sub

	End Class
End Namespace