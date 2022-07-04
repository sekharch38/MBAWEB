Imports System.Drawing

Public Class FrmReportViewer
    Public oReport As Object

    Public Sub New()
        'MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call

    End Sub




    Private Sub FrmReportViewer_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        crViewer.SetBounds(0, 0, Me.Width, Me.Height)
    End Sub







    Private Sub crViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' strUserNameMNU()
        'If UCase(strUserNameMNU) = "ADMIN" Then

        'End If

        'crViewer.BackgroundImage = Image.FromFile("D:\Icons\logo")
        'crViewer.DisplayBackgroundEdge = True
    End Sub


   
End Class