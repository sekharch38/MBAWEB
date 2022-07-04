Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Imports System.Drawing.Printing
Public Class CustomizedReport
    Dim frm As New FrmReportViewer
    Private MyReport As ReportDocument
    Public sMsgCaption As String = "Message"
    Public Sub CustomizedReportSheet(ByVal strUSer As String, ByVal query As String, ByVal Con As String, ByVal Param As Object, ByVal ParamCount As Integer, ByVal UserType As Integer, ByVal Program As String, ByVal Course As String, ByVal Section As String, ByVal Batch As String, ByVal Year As String, ByVal Quota As String, ByVal Category As String, ByVal Hostel As String, ByVal Status As String, ByVal Gender As String)
        Dim reportDocument As ReportDocument
        Dim paramFields As ParameterFields
        Dim paramField As ParameterField
        Dim Ds As New Fees
        Dim paramDiscreteValue As ParameterDiscreteValue
        reportDocument = New ReportDocument()
        paramFields = New ParameterFields()
        Dim CryStudentdetails As New rptCustomizedReport
        Dim Conn As New SqlConnection(Con)
        Dim adepter As New SqlDataAdapter(query, Conn)
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        adepter.Fill(Ds, "StudenInformation")
        DaLogo.Fill(Ds.Vw_Logo)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        Dim RepComm As String = ""
        RepComm = "Student Details"
        Dim Var_Param(,) As String = Param
        If Not Var_Param Is Nothing Then
            For i = 0 To UBound(Var_Param)
                If Not Var_Param(i, 0) Is Nothing Then
                    paramField = New ParameterField()
                    paramField.Name = Var_Param(i, 0)
                    paramDiscreteValue = New ParameterDiscreteValue()
                    paramDiscreteValue.Value = Var_Param(i, 1)
                    paramField.CurrentValues.Add(paramDiscreteValue)
                    'Add the paramField to paramFields
                    paramFields.Add(paramField)
                End If
            Next
        End If

        'if there is any remaining parameter, assign empty value for that 
        'parameter.
        For i As Integer = ParamCount To 6
            ParamCount += 1
            paramField = New ParameterField()
            paramField.Name = "col" + ParamCount.ToString()
            paramDiscreteValue = New ParameterDiscreteValue()
            paramDiscreteValue.Value = ""
            paramField.CurrentValues.Add(paramDiscreteValue)
            'Add the paramField to paramFields
            paramFields.Add(paramField)
        Next

        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetParameterValue("UserName", strUSer)
        CryStudentdetails.SetParameterValue("Program", Program)
        CryStudentdetails.SetParameterValue("Course", Course)
        CryStudentdetails.SetParameterValue("Section", Section)
        CryStudentdetails.SetParameterValue("Batch", Batch)
        CryStudentdetails.SetParameterValue("Year", Year)
        CryStudentdetails.SetParameterValue("Quota", Quota)
        CryStudentdetails.SetParameterValue("Category", Category)
        CryStudentdetails.SetParameterValue("Hostel", Hostel)
        CryStudentdetails.SetParameterValue("Status", Status)
        CryStudentdetails.SetParameterValue("Gender", Gender)

        frm.crViewer.ParameterFieldInfo = paramFields
        EnableButtons(UserType)
        frm.Show()
    End Sub
   
   
    Private Sub EnableButtons(ByVal UT As Integer)
        If UT = 3 Then
            frm.crViewer.EnableToolTips = False
            frm.crViewer.ShowPrintButton = False
            frm.crViewer.ShowExportButton = False
        End If
    End Sub
End Class
