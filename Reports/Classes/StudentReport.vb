Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Imports System.Drawing.Printing
Imports System.Globalization

Public Class StudentReport

    Dim frm As New FrmReportViewer
    Private MyReport As ReportDocument
    Public sMsgCaption As String = "Message"

    Public Sub StudentYearGraph(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentYearGraph As New rptStudentYearGraph
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Year.ToString & " Year Student Year Graph"
        Dim Da As New FeesTableAdapters.Vw_StudentPayDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        ' Dim DaFamily As New FeesTableAdapters.View_FamilyDetailsTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        ' Da.FillByYear(Ds.Vw_StudentPayDetails, Year)
        CryStudentYearGraph.SummaryInfo.ReportComments = RepComm
        CryStudentYearGraph.Subreports.Item("YearChart.rpt").SetDataSource(Ds.Tables.Item("Vw_studentPaydetails"))
        CryStudentYearGraph.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "StudentYearGraph"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentYearGraph
        ' frm.oReport = CryStudentFamily
        EnableButtons(UserType)
        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentDue(ByVal strUSer As String, ByVal StudentId As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptNoDueCertificate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Due Details"
        Dim DSE As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim Da As New FeesTableAdapters.Vw_DueTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        DSE.FillByStudent(Ds.Vw_StudentElements, StudentId)
        Da.Fill(Ds.Vw_Due)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.Subreports.Item("rptDueList.rpt").SetDataSource(Ds.Tables.Item("Vw_Due"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Student Due"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(strUSer)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentVerification(ByVal strUSer As String, ByVal StudentId As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptVerification
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Verification Details"
        Dim DSE As New FeesTableAdapters.Vw_VerificationTableAdapter
        'Dim Da As New FeesTableAdapters.Vw_DueTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        DSE.FillByStudent(Ds.Vw_Verification, StudentId)
        'Da.Fill(Ds.Vw_Due)
        DaCollege.Fill(Ds.Vw_College)
        'CryStudentdetails.Subreports.Item("rptDueList.rpt").SetDataSource(Ds.Tables.Item("Vw_Due"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Student Due"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(strUSer)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentReport(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Details"
        Dim Da As New FeesTableAdapters.Vw_StudentDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentDetails)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(strUSer)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub ActiveStudentsReport(ByVal strUSer As String, ByVal UN As String, ByVal YearCode As String, ByVal State As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCurrentActiveStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Financial Year Student Due Report"
        Dim Da As New FeesTableAdapters.Vw_ActiveStudentsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If State = "Total Active" Then
            Da.FillByTotal(Ds.Vw_ActiveStudents, YearCode)
        ElseIf State = "Due Active" Then

            Da.FillByDue(Ds.Vw_ActiveStudents, YearCode)
        Else
            Da.FillByReceived(Ds.Vw_ActiveStudents, YearCode)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(strUSer)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Function ActiveStudentsReportPdf(ByVal strUSer As String, ByVal UN As String, ByVal YearCode As String, ByVal State As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCurrentActiveStudents
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Financial Year Student Due Report"
        Dim Da As New FeesTableAdapters.Vw_ActiveStudentsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        If State = "Total Active" Then
            Da.FillByTotal(Ds.Vw_ActiveStudents, YearCode)
        ElseIf State = "Due Active" Then

            Da.FillByDue(Ds.Vw_ActiveStudents, YearCode)
        Else
            Da.FillByReceived(Ds.Vw_ActiveStudents, YearCode)

        End If
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(strUSer)
        'frm.Show()
        'frm.Dispose()
        ' Return Cry
        Return CryStudentdetails
    End Function

    Public Function StudentBatchAnalysisPdf(ByVal UserType As Integer, ByVal Batch As String, ByVal UN As String)
        ' frm.crViewer.Dispose()
        'frm.crViewer = Nothing
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentReligion As New rptBatchAnalysisReport
        Dim Ds As New Attendance
        Dim RepComm As String = ""
        RepComm = "Batch Analysis Report"
        Dim Da As New AttendanceTableAdapters.tbl_AcademicYearTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.tbl_AcademicYear, Batch)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentReligion.SummaryInfo.ReportComments = RepComm

        CryStudentReligion.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Batch Analysis Report"
        'frm.crViewer.DisplayGroupTree = True
        CryStudentReligion.SetParameterValue("UserName", UN)
        frm.crViewer.ReportSource = CryStudentReligion
        frm.oReport = CryStudentReligion
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentReligion
        ' Return Cry
    End Function

    Public Sub StudentCustodianReport2(ByVal strUSer As String, ByVal StudentId As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCustodianCertificate2
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaCList As New FeesTableAdapters.Vw_CustodianReportTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudent(Ds.Vw_StudentElements, StudentId)
        DaCollege.Fill(Ds.Vw_College)
        DaCList.Fill(Ds.Vw_CustodianReport)
        CryStudentdetails.Subreports.Item("rptCustodiamReportList.rpt").SetDataSource(Ds.Tables.Item("Vw_CustodianReport"))
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(strUSer)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentCustodianReport(ByVal strUSer As String, ByVal StudentId As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptCustodianCertificate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudent(Ds.Vw_StudentElements, StudentId)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(strUSer)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentTobeRefundReport(ByVal strUSer As String, ByVal Status As String, ByVal UN As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptToBeRefund
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student " & Status & " Details"
        Dim Da As New FeesTableAdapters.Vw_TobeRefundTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStatus(Ds.Vw_TobeRefund, Status)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)

        EnableButtons(strUSer)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeeDivertReport(ByVal strUSer As String, ByVal Status As String, ByVal UN As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptFeeDivert
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student To Be Refund Details"
        Dim Da As New FeesTableAdapters.Vw_FeeDivertTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_FeeDivert)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(strUSer)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentYearReport(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Status As String, ByVal UN As String)

        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        'Here rptStudentReport is the report name of the Crystal reprot declared here.
        Dim CryStudentdetails As New rptStudentReport

        'Here Fees is the Dataset and this dataset contains all the table adapter with Methods for calling the Data.
        Dim Ds As New Fees

        'It declares the Report comment'
        Dim RepComm As String = ""
        RepComm = Year & " Year Student Details"

        'Here it is the declaration of Table Adapters for connecting database data'
        Dim Da As New FeesTableAdapters.Vw_StudentDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        'The Below Statement is used for fill the data in to data set by using adapters of the methods. 
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearStatus(Ds.Vw_StudentDetails, Year, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        'Here the the dataset ds is set to crystal report data source.
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True

        'Here crystal report is assigned to Crystal report viewer.
        frm.crViewer.ReportSource = CryStudentdetails

        'Here the parameteres is passing to  crystal reports.
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()

    End Sub


    Public Function StudentYearReportPdf(ByVal UserType As Integer, ByVal Program As String, ByVal Year As Integer, ByVal Status As String, ByVal UN As String)

        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        'Here rptStudentReport is the report name of the Crystal reprot declared here.
        Dim CryStudentdetails As New rptStudentReport

        'Here Fees is the Dataset and this dataset contains all the table adapter with Methods for calling the Data.
        Dim Ds As New Fees

        'It declares the Report comment'
        Dim RepComm As String = ""
        RepComm = Year & " Year Student Details"

        'Here it is the declaration of Table Adapters for connecting database data'
        Dim Da As New FeesTableAdapters.Vw_StudentDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        'The Below Statement is used for fill the data in to data set by using adapters of the methods. 
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearStatus(Ds.Vw_StudentDetails, Year, Status, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        'Here the the dataset ds is set to crystal report data source.
        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True

        'Here crystal report is assigned to Crystal report viewer.
        frm.crViewer.ReportSource = CryStudentdetails

        'Here the parameteres is passing to  crystal reports.
        CryStudentdetails.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        'frm.Show()
        Return CryStudentdetails

    End Function


    Public Sub StudentNextYearReport(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentdetails As New rptStudentReport
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Year + 1 & " Year Student Details"
        Dim Da As New FeesTableAdapters.Vw_StudentDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentDetails, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentdetails.SummaryInfo.ReportComments = RepComm

        CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentdetails
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentContact(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentContactDetails As New rptStudentContactDetail
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Contact Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentElements)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentContactDetails.SummaryInfo.ReportComments = RepComm

        CryStudentContactDetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Contact Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentContactDetails
        EnableButtons(strUSer)

        frm.Show()
        ' frm.Dispose()
        ' Return Cry
    End Sub
    Public Sub StudentYearContact(ByVal UserType As Integer, ByVal Year As Integer, ByVal Batch As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentContactDetails As New rptStudentContactDetail
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Contact Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatchYear(Ds.Vw_StudentElements, Batch, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentContactDetails.SummaryInfo.ReportComments = RepComm

        CryStudentContactDetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Contact Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentContactDetails
        EnableButtons(UserType)

        frm.Show()
        ' frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub StudentAcademic(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentAcademic As New rptacademic
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Academic Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaAcademic As New FeesTableAdapters.View_AcademicDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentElements)
        DaCollege.Fill(Ds.Vw_College)
        DaAcademic.Fill(Ds.View_AcademicDetails)
        CryStudentAcademic.SummaryInfo.ReportComments = RepComm

        CryStudentAcademic.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Academic Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentAcademic
        EnableButtons(strUSer)

        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentYearAcademic(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentAcademic As New rptacademic
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Academic Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaAcademic As New FeesTableAdapters.View_AcademicDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentElements, Year)
        DaCollege.Fill(Ds.Vw_College)
        DaAcademic.FillByYear(Ds.View_AcademicDetails, Year)
        CryStudentAcademic.SummaryInfo.ReportComments = RepComm

        CryStudentAcademic.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Academic Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentAcademic
        EnableButtons(UserType)

        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentFamily(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentFamily As New rptFamily
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Family Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaFamily As New FeesTableAdapters.View_FamilyDetailsTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentElements)
        DaCollege.Fill(Ds.Vw_College)
        DaFamily.Fill(Ds.View_FamilyDetails)
        CryStudentFamily.SummaryInfo.ReportComments = RepComm

        CryStudentFamily.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Family Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentFamily
        ' frm.oReport = CryStudentFamily
        EnableButtons(strUSer)

        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentYearFamily(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentFamily As New rptFamily
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Family Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        Dim DaFamily As New FeesTableAdapters.View_FamilyDetailsTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentElements, Year)
        DaCollege.Fill(Ds.Vw_College)
        DaFamily.FillByYear(Ds.View_FamilyDetails, Year)
        CryStudentFamily.SummaryInfo.ReportComments = RepComm

        CryStudentFamily.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Family Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentFamily
        ' frm.oReport = CryStudentFamily
        EnableButtons(UserType)

        frm.Show()
        ' Return Cry
    End Sub


    Public Sub StudentBloodGroup(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentBloodGroup As New rptBloodGroup
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Blod Group Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentElements)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentBloodGroup.SummaryInfo.ReportComments = RepComm

        CryStudentBloodGroup.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Blod Group Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentBloodGroup
        frm.oReport = CryStudentBloodGroup
        EnableButtons(strUSer)

        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentYearBloodGroup(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentBloodGroup As New rptBloodGroup
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = Year.ToString & " Year Student Blood Group Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentElements, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentBloodGroup.SummaryInfo.ReportComments = RepComm

        CryStudentBloodGroup.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Blood Group Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentBloodGroup
        frm.oReport = CryStudentBloodGroup
        EnableButtons(UserType)

        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentLanguages(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        'frm.crViewer = Nothing
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentLanguages As New rptStudentLanguages
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Languages Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentElements)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentLanguages.SummaryInfo.ReportComments = RepComm

        CryStudentLanguages.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Languages Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentLanguages
        frm.oReport = CryStudentLanguages
        EnableButtons(strUSer)

        frm.Show()



        ' Return Cry
    End Sub

    Public Sub StudentYearLanguages(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        'frm.crViewer = Nothing
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentLanguages As New rptStudentLanguages
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Languages Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentElements, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentLanguages.SummaryInfo.ReportComments = RepComm

        CryStudentLanguages.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Languages Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentLanguages
        frm.oReport = CryStudentLanguages
        EnableButtons(UserType)

        frm.Show()



        ' Return Cry
    End Sub


    Public Sub StudentReligion(ByVal strUSer As String)
        ' frm.crViewer.Dispose()
        'frm.crViewer = Nothing
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentReligion As New rptStudentReligion
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Religion Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_StudentElements)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentReligion.SummaryInfo.ReportComments = RepComm

        CryStudentReligion.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Religion Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentReligion
        frm.oReport = CryStudentReligion
        EnableButtons(strUSer)
        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentYearReligion(ByVal UserType As Integer, ByVal Year As Integer)
        ' frm.crViewer.Dispose()
        'frm.crViewer = Nothing
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryStudentReligion As New rptStudentReligion
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Student Religion Details"
        Dim Da As New FeesTableAdapters.Vw_StudentElementsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYear(Ds.Vw_StudentElements, Year)
        DaCollege.Fill(Ds.Vw_College)
        CryStudentReligion.SummaryInfo.ReportComments = RepComm

        CryStudentReligion.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Students Student Religion Details"
        'frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryStudentReligion
        frm.oReport = CryStudentReligion
        EnableButtons(UserType)
        frm.Show()
        ' Return Cry
    End Sub

    Public Sub StudentRegister(ByVal UserType As Integer, ByVal BatchId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentRegister
        Dim Ds As New Fees
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaYearFee As New FeesTableAdapters.Vw_YearFeeDetailsTableAdapter
        Dim DaYearFee2 As New FeesTableAdapters.Vw_YearFeeDetails1TableAdapter
        Dim DaYearFee3 As New FeesTableAdapters.Vw_YearFeeDetails2TableAdapter
        Dim DaYearFee4 As New FeesTableAdapters.Vw_YearFeeDetails3TableAdapter
        Dim DaSR As New FeesTableAdapters.Vw_SRTableAdapter
        Dim DaSR1 As New FeesTableAdapters.Vw_SR1TableAdapter
        Dim DaSR2 As New FeesTableAdapters.Vw_SR2TableAdapter
        Dim DaSR3 As New FeesTableAdapters.Vw_SR3TableAdapter

        Dim DaRegpay As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaRegpay2 As New FeesTableAdapters.Vw_StudentPayTransactions1TableAdapter
        Dim DaRegpay3 As New FeesTableAdapters.Vw_StudentPayTransactions2TableAdapter
        Dim DaRegpay4 As New FeesTableAdapters.Vw_StudentPayTransactions3TableAdapter
        Dim DaChart As New FeesTableAdapters.Vw_FeeChartTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByBatch(Ds.Vw_StudentRegister, BatchId)
        DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 1)
        DaYearFee2.FillByYear(Ds.Vw_YearFeeDetails1, 2)
        DaYearFee3.FillByYear(Ds.Vw_YearFeeDetails2, 3)
        DaYearFee4.FillByYear(Ds.Vw_YearFeeDetails3, 4)
        DaSR.FillByYear(Ds.Vw_SR, 1)
        DaSR1.FillByYear(Ds.Vw_SR1, 2)
        DaSR2.FillByYear(Ds.Vw_SR2, 3)
        DaSR3.FillByYear(Ds.Vw_SR3, 4)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)


        DaRegpay.FillByYear(Ds.Vw_StudentPayTransactions, 1)
        DaRegpay2.FillByYear(Ds.Vw_StudentPayTransactions1, 2)

        DaRegpay3.FillByYear(Ds.Vw_StudentPayTransactions2, 3)
        DaRegpay4.FillByYear(Ds.Vw_StudentPayTransactions3, 4)


        DaLogo.Fill(Ds.Vw_Logo)
        Cry.Subreports.Item("rptYearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails"))
        Cry.Subreports.Item("rpt2YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails1"))
        Cry.Subreports.Item("rpt3YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails2"))
        Cry.Subreports.Item("rpt4YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails3"))
        Cry.Subreports.Item("rptSR.rpt").SetDataSource(Ds.Tables.Item("Vw_SR"))
        Cry.Subreports.Item("rptSR1.rpt").SetDataSource(Ds.Tables.Item("Vw_SR1"))
        Cry.Subreports.Item("rptSR2.rpt").SetDataSource(Ds.Tables.Item("Vw_SR2"))
        Cry.Subreports.Item("rptSR3.rpt").SetDataSource(Ds.Tables.Item("Vw_SR3"))

        Cry.Subreports.Item("rptRegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions"))
        If Ds.Vw_StudentPayTransactions1.Rows.Count > 0 Then
            Cry.Subreports.Item("rpt2RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions1"))

            Dim subreport As New rpt2RegisterPayDetails




        End If
        Cry.Subreports.Item("rpt3RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions2"))
        Cry.Subreports.Item("rpt4RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions3"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub

    'Public Sub StudentYearDues(ByVal UserType As Integer, ByVal BatchId As String, ByVal Actual As String)
    '    frm.crViewer.ReportSource = Nothing
    '    frm.crViewer.Refresh()
    '    Dim Cry As New rptStudentYearWiseDueReport
    '    Dim Ds As New Fees
    '    Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentDetailsTableAdapter
    '    Dim DaYearFeeDue As New FeesTableAdapters.Vw_StudentYearDueTableAdapter
    '    Dim DaYearFee2Due As New FeesTableAdapters.Vw_StudentYearDue1TableAdapter
    '    Dim DaYearFee3Due As New FeesTableAdapters.Vw_StudentYearDue2TableAdapter
    '    Dim DaYearFee4Due As New FeesTableAdapters.Vw_StudentYearDue3TableAdapter

    '    Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
    '    Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

    '    DaCollege.Fill(Ds.Vw_College)
    '    DaStudentRegister.FillByYear(Ds.Vw_StudentDetails, BatchId)
    '    DaYearFeeDue.FillByYear(Ds.Vw_StudentYearDue, 1, Actual)
    '    DaYearFee2Due.FillByYear(Ds.Vw_StudentYearDue1, 2, Actual)
    '    DaYearFee3Due.FillByYear(Ds.Vw_StudentYearDue2, 3, Actual)
    '    DaYearFee4Due.FillByYear(Ds.Vw_StudentYearDue3, 4, Actual)


    '    DaLogo.Fill(Ds.Vw_Logo)
    '    Cry.Subreports.Item("rptFirstYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentYearDue"))
    '    Cry.Subreports.Item("rptSecondYearDue.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentYearDue1"))
    '    Cry.Subreports.Item("rptThirdYear.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentYearDue2"))
    '    Cry.Subreports.Item("rptFourthYear.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentYearDue3"))


    '    Cry.SetDataSource(DirectCast(Ds, DataSet))
    '    frm.crViewer.ReportSource = Cry
    '    frm.oReport = Cry
    '    EnableButtons(UserType)
    '    frm.Show()
    'End Sub

    Public Sub StudentBatchRegister(ByVal UserType As Integer, ByVal BatchId As String, ByVal StudentId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentRegister
        Dim Ds As New Fees
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaYearFee As New FeesTableAdapters.Vw_YearFeeDetailsTableAdapter
        Dim DaYearFee2 As New FeesTableAdapters.Vw_YearFeeDetails1TableAdapter
        Dim DaYearFee3 As New FeesTableAdapters.Vw_YearFeeDetails2TableAdapter
        Dim DaYearFee4 As New FeesTableAdapters.Vw_YearFeeDetails3TableAdapter
        Dim DaSR As New FeesTableAdapters.Vw_SRTableAdapter
        Dim DaSR1 As New FeesTableAdapters.Vw_SR1TableAdapter
        Dim DaSR2 As New FeesTableAdapters.Vw_SR2TableAdapter
        Dim DaSR3 As New FeesTableAdapters.Vw_SR3TableAdapter

        Dim DaRegpay As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaRegpay2 As New FeesTableAdapters.Vw_StudentPayTransactions1TableAdapter
        Dim DaRegpay3 As New FeesTableAdapters.Vw_StudentPayTransactions2TableAdapter
        Dim DaRegpay4 As New FeesTableAdapters.Vw_StudentPayTransactions3TableAdapter
        Dim DaChart As New FeesTableAdapters.Vw_FeeChartTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByStudentBatch(Ds.Vw_StudentRegister, BatchId, StudentId)
        DaYearFee.FillByStudentYear(Ds.Vw_YearFeeDetails, 1, StudentId)
        DaYearFee2.FillByStudentYear(Ds.Vw_YearFeeDetails1, 2, StudentId)
        DaYearFee3.FillByStudentYear(Ds.Vw_YearFeeDetails2, 3, StudentId)
        DaYearFee4.FillByStudentYear(Ds.Vw_YearFeeDetails3, 4, StudentId)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)
        DaSR.FillByYear(Ds.Vw_SR, 1)
        DaSR1.FillByYear(Ds.Vw_SR1, 2)
        DaSR2.FillByYear(Ds.Vw_SR2, 3)
        DaSR3.FillByYear(Ds.Vw_SR3, 4)

        DaRegpay.FillByStudentYear(Ds.Vw_StudentPayTransactions, 1, StudentId)
        DaRegpay2.FillByStudentYear(Ds.Vw_StudentPayTransactions1, 2, StudentId)

        DaRegpay3.FillByStudentYear(Ds.Vw_StudentPayTransactions2, 3, StudentId)
        DaRegpay4.FillByStudentYear(Ds.Vw_StudentPayTransactions3, 4, StudentId)


        DaLogo.Fill(Ds.Vw_Logo)
        Cry.Subreports.Item("rptYearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails"))
        Cry.Subreports.Item("rpt2YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails1"))
        Cry.Subreports.Item("rpt3YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails2"))
        Cry.Subreports.Item("rpt4YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails3"))
        'Cry.Subreports.Item("rptYearChart.rpt").SetDataSource(Ds.Tables.Item("Vw_FeeChart"))
        Cry.Subreports.Item("rptSR.rpt").SetDataSource(Ds.Tables.Item("Vw_SR"))
        Cry.Subreports.Item("rptSR1.rpt").SetDataSource(Ds.Tables.Item("Vw_SR1"))
        Cry.Subreports.Item("rptSR2.rpt").SetDataSource(Ds.Tables.Item("Vw_SR2"))
        Cry.Subreports.Item("rptSR3.rpt").SetDataSource(Ds.Tables.Item("Vw_SR3"))
        Cry.Subreports.Item("rptRegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions"))
        If Ds.Vw_StudentPayTransactions1.Rows.Count > 0 Then
            Cry.Subreports.Item("rpt2RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions1"))

            Dim subreport As New rpt2RegisterPayDetails




        End If
        Cry.Subreports.Item("rpt3RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions2"))
        Cry.Subreports.Item("rpt4RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions3"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub
    Public Sub StudentCourseRegister(ByVal UserType As Integer, ByVal BatchId As String, ByVal course As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentRegister
        Dim Ds As New Fees
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaYearFee As New FeesTableAdapters.Vw_YearFeeDetailsTableAdapter
        Dim DaYearFee2 As New FeesTableAdapters.Vw_YearFeeDetails1TableAdapter
        Dim DaYearFee3 As New FeesTableAdapters.Vw_YearFeeDetails2TableAdapter
        Dim DaYearFee4 As New FeesTableAdapters.Vw_YearFeeDetails3TableAdapter

        Dim DaRegpay As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaRegpay2 As New FeesTableAdapters.Vw_StudentPayTransactions1TableAdapter
        Dim DaRegpay3 As New FeesTableAdapters.Vw_StudentPayTransactions2TableAdapter
        Dim DaRegpay4 As New FeesTableAdapters.Vw_StudentPayTransactions3TableAdapter
        Dim DaChart As New FeesTableAdapters.Vw_FeeChartTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByCourseBatch(Ds.Vw_StudentRegister, BatchId, course)
        DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 1)
        DaYearFee2.FillByYear(Ds.Vw_YearFeeDetails1, 2)
        DaYearFee3.FillByYear(Ds.Vw_YearFeeDetails2, 3)
        DaYearFee4.FillByYear(Ds.Vw_YearFeeDetails3, 4)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)


        DaRegpay.FillByYear(Ds.Vw_StudentPayTransactions, 1)
        DaRegpay2.FillByYear(Ds.Vw_StudentPayTransactions1, 2)

        DaRegpay3.FillByYear(Ds.Vw_StudentPayTransactions2, 3)
        DaRegpay4.FillByYear(Ds.Vw_StudentPayTransactions3, 4)


        DaLogo.Fill(Ds.Vw_Logo)
        Cry.Subreports.Item("rptYearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails"))
        Cry.Subreports.Item("rpt2YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails1"))
        Cry.Subreports.Item("rpt3YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails2"))
        Cry.Subreports.Item("rpt4YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails3"))
        'Cry.Subreports.Item("rptYearChart.rpt").SetDataSource(Ds.Tables.Item("Vw_FeeChart"))

        Cry.Subreports.Item("rptRegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions"))
        If Ds.Vw_StudentPayTransactions1.Rows.Count > 0 Then
            Cry.Subreports.Item("rpt2RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions1"))

            Dim subreport As New rpt2RegisterPayDetails




        End If
        Cry.Subreports.Item("rpt3RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions2"))
        Cry.Subreports.Item("rpt4RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions3"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub
    Public Sub StudentQuotaRegister(ByVal UserType As Integer, ByVal BatchId As String, ByVal Quota As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentRegister
        Dim Ds As New Fees
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaYearFee As New FeesTableAdapters.Vw_YearFeeDetailsTableAdapter
        Dim DaYearFee2 As New FeesTableAdapters.Vw_YearFeeDetails1TableAdapter
        Dim DaYearFee3 As New FeesTableAdapters.Vw_YearFeeDetails2TableAdapter
        Dim DaYearFee4 As New FeesTableAdapters.Vw_YearFeeDetails3TableAdapter

        Dim DaRegpay As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaRegpay2 As New FeesTableAdapters.Vw_StudentPayTransactions1TableAdapter
        Dim DaRegpay3 As New FeesTableAdapters.Vw_StudentPayTransactions2TableAdapter
        Dim DaRegpay4 As New FeesTableAdapters.Vw_StudentPayTransactions3TableAdapter
        Dim DaChart As New FeesTableAdapters.Vw_FeeChartTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByQuotaBatch(Ds.Vw_StudentRegister, BatchId, Quota)
        DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 1)
        DaYearFee2.FillByYear(Ds.Vw_YearFeeDetails1, 2)
        DaYearFee3.FillByYear(Ds.Vw_YearFeeDetails2, 3)
        DaYearFee4.FillByYear(Ds.Vw_YearFeeDetails3, 4)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)


        DaRegpay.FillByYear(Ds.Vw_StudentPayTransactions, 1)
        DaRegpay2.FillByYear(Ds.Vw_StudentPayTransactions1, 2)

        DaRegpay3.FillByYear(Ds.Vw_StudentPayTransactions2, 3)
        DaRegpay4.FillByYear(Ds.Vw_StudentPayTransactions3, 4)


        DaLogo.Fill(Ds.Vw_Logo)
        Cry.Subreports.Item("rptYearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails"))
        Cry.Subreports.Item("rpt2YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails1"))
        Cry.Subreports.Item("rpt3YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails2"))
        Cry.Subreports.Item("rpt4YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails3"))
        'Cry.Subreports.Item("rptYearChart.rpt").SetDataSource(Ds.Tables.Item("Vw_FeeChart"))

        Cry.Subreports.Item("rptRegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions"))
        If Ds.Vw_StudentPayTransactions1.Rows.Count > 0 Then
            Cry.Subreports.Item("rpt2RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions1"))

            Dim subreport As New rpt2RegisterPayDetails




        End If
        Cry.Subreports.Item("rpt3RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions2"))
        Cry.Subreports.Item("rpt4RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions3"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub
    Public Sub StudentEntryRegister(ByVal UserType As Integer, ByVal BatchId As String, ByVal Entry As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentRegister
        Dim Ds As New Fees
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaYearFee As New FeesTableAdapters.Vw_YearFeeDetailsTableAdapter
        Dim DaYearFee2 As New FeesTableAdapters.Vw_YearFeeDetails1TableAdapter
        Dim DaYearFee3 As New FeesTableAdapters.Vw_YearFeeDetails2TableAdapter
        Dim DaYearFee4 As New FeesTableAdapters.Vw_YearFeeDetails3TableAdapter

        Dim DaRegpay As New FeesTableAdapters.Vw_StudentPayTransactionsTableAdapter
        Dim DaRegpay2 As New FeesTableAdapters.Vw_StudentPayTransactions1TableAdapter
        Dim DaRegpay3 As New FeesTableAdapters.Vw_StudentPayTransactions2TableAdapter
        Dim DaRegpay4 As New FeesTableAdapters.Vw_StudentPayTransactions3TableAdapter
        Dim DaChart As New FeesTableAdapters.Vw_FeeChartTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByStudentEntry(Ds.Vw_StudentRegister, BatchId, Entry)
        DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 1)
        DaYearFee2.FillByYear(Ds.Vw_YearFeeDetails1, 2)
        DaYearFee3.FillByYear(Ds.Vw_YearFeeDetails2, 3)
        DaYearFee4.FillByYear(Ds.Vw_YearFeeDetails3, 4)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)


        DaRegpay.FillByYear(Ds.Vw_StudentPayTransactions, 1)
        DaRegpay2.FillByYear(Ds.Vw_StudentPayTransactions1, 2)

        DaRegpay3.FillByYear(Ds.Vw_StudentPayTransactions2, 3)
        DaRegpay4.FillByYear(Ds.Vw_StudentPayTransactions3, 4)


        DaLogo.Fill(Ds.Vw_Logo)
        Cry.Subreports.Item("rptYearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails"))
        Cry.Subreports.Item("rpt2YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails1"))
        Cry.Subreports.Item("rpt3YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails2"))
        Cry.Subreports.Item("rpt4YearFeeDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_YearFeeDetails3"))
        'Cry.Subreports.Item("rptYearChart.rpt").SetDataSource(Ds.Tables.Item("Vw_FeeChart"))

        Cry.Subreports.Item("rptRegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions"))
        If Ds.Vw_StudentPayTransactions1.Rows.Count > 0 Then
            Cry.Subreports.Item("rpt2RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions1"))

            Dim subreport As New rpt2RegisterPayDetails




        End If
        Cry.Subreports.Item("rpt3RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions2"))
        Cry.Subreports.Item("rpt4RegisterPayDetails.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentPayTransactions3"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub


    Public Sub StudentBatchYearConsolidate(ByVal UserType As Integer, ByVal BatchId As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptBatchStudentYearConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = BatchId & " " & "Student Year Consolidation"
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaStudents As New FeesTableAdapters.Vw_YCQE_NOSTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByBatch(Ds.Vw_StudentRegister, BatchId)
        DaStudents.FillByBatch(Ds.Vw_YCQE_NOS, BatchId)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)



        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.Subreports.Item("rptBatchStudentAllYearConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_YCQE_NOS"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub StudentBatchEntryConsolidate(ByVal UserType As Integer, ByVal BatchId As String, ByVal EntryId As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptBatchStudentYearConsolidate
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = BatchId & " " & "Student" & EntryId & "  Consolidation"
        Dim DaStudentRegister As New FeesTableAdapters.Vw_StudentRegisterTableAdapter
        Dim DaStudents As New FeesTableAdapters.Vw_YCQE_NOSTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentRegister.FillByStudentEntry(Ds.Vw_StudentRegister, BatchId, EntryId)
        DaStudents.FillByBatch(Ds.Vw_YCQE_NOS, BatchId)
        'DaChart.Fill(Ds.Vw_FeeChart)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 3)
        'DaYearFee.FillByYear(Ds.Vw_YearFeeDetails, 4)



        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.Subreports.Item("rptBatchStudentAllYearConsolidate.rpt").SetDataSource(Ds.Tables.Item("Vw_YCQE_NOS"))

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub



    Public Sub StudentTC(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String, ByVal Copy As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptTC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        ' RepComm = BatchId & " " & "Student" & EntryId & "  Consolidation"
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudentTC(Ds.Vw_Certificates, StudentId, CTID, Copy)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        'For Viewer'
        'Dim Msg As Object
        'Msg = MsgBox("Do you want to Print Preview?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, sMsgCaption)
        'If Msg = MsgBoxResult.No Then
        '    Cry.PrintToPrinter(1, False, 0, 0)
        '    frm.crViewer.ReportSource = Cry

        'Else
        '    frm.crViewer.ReportSource = Cry
        '    frm.oReport = Cry
        '    EnableButtonsTC()
        '    frm.Show()

        '    Msg = MsgBox("Do you want to Print?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, sMsgCaption)
        '    If Msg = MsgBoxResult.No Then
        '        Exit Sub
        '    Else
        '        Cry.PrintToPrinter(1, False, 0, 0)
        '        frm.crViewer.ReportSource = Cry
        '    End If
        'End If


        'End Viewer'
        Cry.PrintToPrinter(1, False, 0, 0)
        frm.crViewer.ReportSource = Cry
        'frm.Show()
    End Sub


    Public Sub StudentTCPreview(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String, ByVal Copy As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptTC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        ' RepComm = BatchId & " " & "Student" & EntryId & "  Consolidation"
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudentTC(Ds.Vw_Certificates, StudentId, CTID, Copy)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        'For Viewer'
        'Dim Msg As Object
        'Msg = MsgBox("Do you want to Print Preview?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, sMsgCaption)
        'If Msg = MsgBoxResult.No Then
        '    Cry.PrintToPrinter(1, False, 0, 0)
        '    frm.crViewer.ReportSource = Cry

        'Else
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtonsTC()


        '    Msg = MsgBox("Do you want to Print?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, sMsgCaption)
        '    If Msg = MsgBoxResult.No Then
        '        Exit Sub
        '    Else
        '        Cry.PrintToPrinter(1, False, 0, 0)
        '        frm.crViewer.ReportSource = Cry
        '    End If
        'End If


        'End Viewer'
        'Cry.PrintToPrinter(1, False, 0, 0)
        'frm.crViewer.ReportSource = Cry
        frm.Show()

    End Sub

    Public Function StudentTCPreviewPdf(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String, ByVal Copy As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptTC
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Ds.EnforceConstraints = False
        ' RepComm = BatchId & " " & "Student" & EntryId & "  Consolidation"
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudentTC(Ds.Vw_Certificates, StudentId, CTID, Copy)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        'For Viewer'
        'Dim Msg As Object
        'Msg = MsgBox("Do you want to Print Preview?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, sMsgCaption)
        'If Msg = MsgBoxResult.No Then
        '    Cry.PrintToPrinter(1, False, 0, 0)
        '    frm.crViewer.ReportSource = Cry

        'Else
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtonsTC()


        '    Msg = MsgBox("Do you want to Print?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, sMsgCaption)
        '    If Msg = MsgBoxResult.No Then
        '        Exit Sub
        '    Else
        '        Cry.PrintToPrinter(1, False, 0, 0)
        '        frm.crViewer.ReportSource = Cry
        '    End If
        'End If


        'End Viewer'
        'Cry.PrintToPrinter(1, False, 0, 0)
        'frm.crViewer.ReportSource = Cry
        'frm.Show()
        Return Cry

    End Function


    Public Sub StudentBC(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptBonafide
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter

        DaStudentCertificates.FillByStudent(Ds.Vw_Certificates, StudentId, CTID)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        Cry.PrintToPrinter(1, False, 0, 0)
        frm.crViewer.ReportSource = Cry
    End Sub
    Public Sub StudentBCPreview(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptBonafide
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudent(Ds.Vw_Certificates, StudentId, CTID)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtonsTC()
        frm.Show()
    End Sub

    Public Function StudentBCPreviewPdf(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptBonafide
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudent(Ds.Vw_Certificates, StudentId, CTID)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtonsTC()
        'frm.Show()
        Return Cry
    End Function

    Public Sub BCPreview(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptBonafidemba
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudent(Ds.Vw_Certificates, StudentId, CTID)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        frm.Show()
    End Sub

    Public Function BCPreviewDoc(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String) As rptBonafidemba
        Dim Cry As New rptBonafidemba
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Ds.EnforceConstraints = False
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_CertificatesTableAdapter
        DaStudentCertificates.FillByStudent(Ds.Vw_Certificates, StudentId, CTID)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        Return Cry

    End Function

    Public Sub StudentUFROBC(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptUFROBonafide
        Dim Ds As New Fees
        Dim RepComm As String = ""
        ' RepComm = BatchId & " " & "Student" & EntryId & "  Consolidation"
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_UFROBCTableAdapter
        'Dim DaStudents As New FeesTableAdapters.Vw_YCQE_NOSTableAdapter
        'Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        'Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        'DaCollege.Fill(Ds.Vw_College)
        'DaStudentRegister.FillByStudentEntry(Ds.Vw_StudentRegister, BatchId, EntryId)
        DaStudentCertificates.FillByStudent(Ds.Vw_UFROBC, StudentId, CTID)
        ' DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        'frm.crViewer.ReportSource = Cry
        'frm.oReport = Cry
        'EnableButtons(UserType)
        'frm.Show()
        Cry.PrintToPrinter(1, False, 0, 0)
        frm.crViewer.ReportSource = Cry
    End Sub

    Public Sub StudentUFROBCPreview(ByVal UserType As Integer, ByVal StudentId As String, ByVal CTID As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptUFROBonafide
        Dim Ds As New Fees
        Dim RepComm As String = ""
        ' RepComm = BatchId & " " & "Student" & EntryId & "  Consolidation"
        Dim DaStudentCertificates As New FeesTableAdapters.Vw_UFROBCTableAdapter
        Ds.EnforceConstraints = False
        DaStudentCertificates.FillByStudent(Ds.Vw_UFROBC, StudentId, CTID)
        Cry.SummaryInfo.ReportComments = RepComm
        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        frm.oReport = Cry
        EnableButtonsTC()
        frm.Show()
    End Sub

    Public Sub BatchStudentRA(ByVal UserType As Integer, ByVal BatchId As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptReAdmission
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = BatchId & " " & "Batch Student ReAdmission Details"
        Dim DaStudentDetain As New FeesTableAdapters.Vw_ReAdmissionTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        DaStudentDetain.FillByBatch(Ds.Vw_ReAdmission, BatchId)




        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm


        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        Cry.SetParameterValue("UserName", UN)
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub BatchStudentDetainDetails(ByVal UserType As Integer, ByVal BatchId As String, ByVal Batch As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentDetainDetails
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = BatchId & " " & "Batch Student Detain Details"
        Dim DaStudentDetain As New FeesTableAdapters.Vw_StudentDetainDetailsTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        If Batch = "Batch" Then
            DaStudentDetain.FillByBatch(Ds.Vw_StudentDetainDetails, BatchId)
        Else
            DaStudentDetain.FillByCurrentBatch(Ds.Vw_StudentDetainDetails, BatchId)
        End If

        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm

        Cry.SetDataSource(DirectCast(Ds, DataSet))

        frm.crViewer.ReportSource = Cry
        Cry.SetParameterValue("UserName", UN)
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub


    Public Sub BatchStudentDetainDetailsFY(ByVal UserType As Integer, ByVal UN As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal Status As String)
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentDetainDetailsFY
        Dim Ds As New Fees
        Dim RepComm As String = ""
        Dim DaStudentDetain As New FeesTableAdapters.Vw_StudentDetainDetailsTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)

        'If Batch = "Batch" Then
        '    DaStudentDetain.FillByBatch(Ds.Vw_StudentDetainDetails, BatchId)
        'Else
        '    DaStudentDetain.FillByCurrentBatch(Ds.Vw_StudentDetainDetails, BatchId)
        'End If
        If Status = "All" Then
            RepComm = "Financial Year Student Detain Details"

            DaStudentDetain.FillByDate(Ds.Vw_StudentDetainDetails, strdt, strEndt)
        Else
            If Status = "Re Admitted" Then
                RepComm = "Financial Year Student Detain Details with Readmisssion"
            Else
                RepComm = "Financial Year Student Detain Details with out Readmission"
            End If

            DaStudentDetain.FillByDateStatus(Ds.Vw_StudentDetainDetails, strdt, strEndt, Status)
        End If


        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm

        Cry.SetDataSource(DirectCast(Ds, DataSet))

        frm.crViewer.ReportSource = Cry
        Cry.SetParameterValue("UserName", UN)
        Cry.SetParameterValue("FromDt", strdt)
        Cry.SetParameterValue("EndDt", strEndt)
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub


  

    Public Sub BatchStudentDetainDetailsWR(ByVal UserType As Integer, ByVal BatchId As String, ByVal Batch As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentDetainDetails
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = BatchId & " " & "Batch Student Detain Details"
        Dim DaStudentDetain As New FeesTableAdapters.Vw_StudentDetainDetailsTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        If Batch = "Batch" Then
            DaStudentDetain.FillByDetainStatus(Ds.Vw_StudentDetainDetails, BatchId, "Re Admitted")
        Else
            DaStudentDetain.FillByDetainStatus(Ds.Vw_StudentDetainDetails, BatchId, "Re Admitted")
        End If
        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        Cry.SetParameterValue("UserName", UN)
        frm.oReport = Cry
        EnableButtons(UserType)
        frm.Show()
    End Sub

    Public Sub BatchStudentDetainDetailsWOR(ByVal UserType As Integer, ByVal BatchId As String, ByVal Batch As String, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Cry As New rptStudentDetainDetails
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = BatchId & " " & "Batch Student Detain Details"
        Dim DaStudentDetain As New FeesTableAdapters.Vw_StudentDetainDetailsTableAdapter

        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter

        DaCollege.Fill(Ds.Vw_College)
        If Batch = "Batch" Then
            DaStudentDetain.FillByDetainStatus(Ds.Vw_StudentDetainDetails, BatchId, "Not ReAdmitted")
        Else
            DaStudentDetain.FillByDetainStatus(Ds.Vw_StudentDetainDetails, BatchId, "Not ReAdmitted")

        End If




        DaLogo.Fill(Ds.Vw_Logo)
        Cry.SummaryInfo.ReportComments = RepComm

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        frm.crViewer.ReportSource = Cry
        Cry.SetParameterValue("UserName", UN)

        frm.oReport = Cry
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
    Private Sub EnableButtonsTC()


        frm.crViewer.EnableToolTips = False
        frm.crViewer.ShowPrintButton = False
        frm.crViewer.ShowExportButton = False


    End Sub




End Class
