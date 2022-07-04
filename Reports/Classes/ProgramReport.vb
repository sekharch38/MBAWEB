
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Public Class ProgramReport
    Dim frm As New FrmReportViewer
    Private MyReport As ReportDocument
    Public Sub ProgramsReport(ByVal UserType As Integer)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryProgramdetails As New rptPrograms
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim Da As New FeesTableAdapters.tbl_fees_ProgramTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_Program)
        DaCollege.Fill(Ds.Vw_College)
        CryProgramdetails.SummaryInfo.ReportComments = RepComm

        CryProgramdetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryProgramdetails
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub CourseDetails(ByVal UserType As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryCourseDetails As New rptCourse
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Course Details"
        Dim Da As New FeesTableAdapters.Vw_CourseDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_CourseDetails)
        DaCollege.Fill(Ds.Vw_College)
        CryCourseDetails.SummaryInfo.ReportComments = RepComm

        CryCourseDetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Courses"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryCourseDetails
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub SubjectDetails(ByVal UserType As Integer, ByVal UN As String)
        Try
            ' frm.crViewer.Dispose()
            frm.crViewer.ReportSource = Nothing
            frm.crViewer.Refresh()
            Dim CrySubjectDetails As New subjectrpt
            Dim Ds As New Fees
            Dim RepComm As String = ""
            RepComm = "Subject Details"
            Dim Da As New FeesTableAdapters.Vw_SubjectDetailsTableAdapter
            Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.Fill(Ds.Vw_SubjectDetails)
            DaCollege.Fill(Ds.Vw_College)
            CrySubjectDetails.SummaryInfo.ReportComments = RepComm
            CrySubjectDetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Subject Details"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CrySubjectDetails
            CrySubjectDetails.SetParameterValue("UserName", UN)
            EnableButtons(UserType)
            frm.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Sub SubjectDetailsWOS(ByVal UserType As Integer, ByVal UN As String)
        Try
            ' frm.crViewer.Dispose()
            frm.crViewer.ReportSource = Nothing
            frm.crViewer.Refresh()
            Dim CrySubjectDetails As New subjectrptWOS
            Dim Ds As New Fees
            Dim RepComm As String = ""
            RepComm = "I Year Subject Details"
            Dim Da As New FeesTableAdapters.Vw_SubjectDetailsWOSTableAdapter
            Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.Fill(Ds.Vw_SubjectDetailsWOS)
            DaCollege.Fill(Ds.Vw_College)
            CrySubjectDetails.SummaryInfo.ReportComments = RepComm
            CrySubjectDetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "I Year Subject Details"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CrySubjectDetails
            CrySubjectDetails.SetParameterValue("UserName", UN)
            EnableButtons(UserType)
            frm.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub


    Public Sub SectionDetails(ByVal UserType As Integer)
        ' frm.crViewer.Dispose()
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CrySectionDetails As New rptSection
        Dim Ds As New Fees
        Dim RepComm As String = ""
        RepComm = "Section Details"
        Dim Da As New FeesTableAdapters.Vw_SectionDetailsTableAdapter
        Dim DaLogo As New FeesTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New FeesTableAdapters.Vw_CollegeTableAdapter
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_SectionDetails)
        DaCollege.Fill(Ds.Vw_College)
        CrySectionDetails.SummaryInfo.ReportComments = RepComm

        CrySectionDetails.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Sections"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CrySectionDetails

        EnableButtons(UserType)

        frm.Show()


    End Sub

    Private Sub EnableButtons(ByVal UserType As Integer)
        If UserType = 3 Then

            frm.crViewer.EnableToolTips = False
            frm.crViewer.ShowPrintButton = False
            frm.crViewer.ShowExportButton = False

        End If
    End Sub



End Class
