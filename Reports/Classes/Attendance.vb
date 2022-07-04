Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports
Imports System.Globalization

Public Class Attendance
    Dim frm As New FrmReportViewer
    Private MyReport As ReportDocument


   


    Public Sub StudentMarksReport(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal Program As String, ByVal Course As String, ByVal Section As String, ByVal SId As Integer, ByVal Year As Integer, ByVal Semester As String, ByVal Test As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim Ds As New Attendance
        Dim RepComm As String = ""
        If Test = "Best Of 3" Then
            Dim CryStudentdetails As New rptStudentMarksReport
            RepComm = "Student Internal Marks"
            Dim Da As New AttendanceTableAdapters.Vw_StudentMarksReportTableAdapter
            Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByBatch(Ds.Vw_StudentMarksReport, Batch, Program, Course, Section, Year, Semester, SId)
            DaCollege.Fill(Ds.Vw_College)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Student Internal Marks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            frm.oReport = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()
        ElseIf Test = "Test 1" Then
            Dim CryStudentdetails As New rptStudentMarksInternal1
            RepComm = Test & " Student Internal Marks"
            Dim Da As New AttendanceTableAdapters.Vw_StudentMarksReportTableAdapter
            Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByBatch(Ds.Vw_StudentMarksReport, Batch, Program, Course, Section, Year, Semester, SId)
            DaCollege.Fill(Ds.Vw_College)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Student Internal Marks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            ' CryStudentdetails.SetParameterValue("UserName", UN)
            frm.oReport = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()

        ElseIf Test = "Test 2" Then
            Dim CryStudentdetails As New rptStudentMarksInternal2
            RepComm = Test & " Student Internal Marks"
            Dim Da As New AttendanceTableAdapters.Vw_StudentMarksReportTableAdapter
            Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByBatch(Ds.Vw_StudentMarksReport, Batch, Program, Course, Section, Year, Semester, SId)
            DaCollege.Fill(Ds.Vw_College)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Student Internal Marks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            ' CryStudentdetails.SetParameterValue("UserName", UN)
            frm.oReport = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()
        ElseIf Test = "Test 3" Then
            Dim CryStudentdetails As New rptStudentMarksInternal3
            RepComm = Test & " Student Internal Marks"
            Dim Da As New AttendanceTableAdapters.Vw_StudentMarksReportTableAdapter
            Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByBatch(Ds.Vw_StudentMarksReport, Batch, Program, Course, Section, Year, Semester, SId)
            DaCollege.Fill(Ds.Vw_College)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Student Internal Marks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            ' CryStudentdetails.SetParameterValue("UserName", UN)
            frm.oReport = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()

        ElseIf Test = "Internal Marks" Then
            Dim CryStudentdetails As New rptStudentInternalMarks
            RepComm = "Student Internal Marks"
            Dim Da As New AttendanceTableAdapters.Vw_StudentMarksReportTableAdapter
            Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
            Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
            DaLogo.Fill(Ds.Vw_Logo)
            Da.FillByStudentInternalMarks(Ds.Vw_StudentMarksReport, Batch, Program, Course, Section, Year, Semester)
            DaCollege.Fill(Ds.Vw_College)
            CryStudentdetails.SummaryInfo.ReportComments = RepComm
            CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            frm.Text = "Student Internal Marks"
            frm.crViewer.DisplayGroupTree = True
            frm.crViewer.ReportSource = CryStudentdetails
            CryStudentdetails.SetParameterValue("UserName", UN)
            CryStudentdetails.SetParameterValue("Batch", Batch)
            CryStudentdetails.SetParameterValue("Program", Program)
            CryStudentdetails.SetParameterValue("Course", Course)
            CryStudentdetails.SetParameterValue("Section", Section)
            CryStudentdetails.SetParameterValue("Year", Year)
            CryStudentdetails.SetParameterValue("Semester", Semester)
            CryStudentdetails.SetParameterValue("Test", Test)
            frm.oReport = CryStudentdetails
            EnableButtons(UserType)
            frm.Show()

            'ElseIf Test = "ExternalMarks" Then
            '    Dim CryStudentdetails As New rptExternalMarks
            '    RepComm = "Student Internal Marks"
            '    Dim Da As New AttendanceTableAdapters.Vw_StTableAdapter
            '    Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
            '    Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
            '    DaLogo.Fill(Ds.Vw_Logo)
            '    Da.FillByStudentInternalMarks(Ds.Vw_StudentMarksReport, Batch, Program, Course, Section, Year, Semester)
            '    DaCollege.Fill(Ds.Vw_College)
            '    CryStudentdetails.SummaryInfo.ReportComments = RepComm
            '    CryStudentdetails.SetDataSource(DirectCast(Ds, DataSet))
            '    frm.Text = "Student Internal Marks"
            '    frm.crViewer.DisplayGroupTree = True
            '    frm.crViewer.ReportSource = CryStudentdetails
            '    CryStudentdetails.SetParameterValue("UserName", UN)
            '    CryStudentdetails.SetParameterValue("Batch", Batch)
            '    CryStudentdetails.SetParameterValue("Program", Program)
            '    CryStudentdetails.SetParameterValue("Course", Course)
            '    CryStudentdetails.SetParameterValue("Section", Section)
            '    CryStudentdetails.SetParameterValue("Year", Year)
            '    CryStudentdetails.SetParameterValue("Semester", Semester)
            '    CryStudentdetails.SetParameterValue("Test", Test)
            '    frm.oReport = CryStudentdetails
            '    EnableButtons(UserType)
            '    frm.Show()


        End If
    End Sub

    Public Function StudentIdCardPDf(ByVal UserType As Integer, ByVal StudentId As String, ByVal Batch As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptStudentIdCard
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "ID - CARD"
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEdt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

        Dim Da As New AttendanceTableAdapters.Vw_StudentId_CardTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        ' Dim DaMess As New AttendanceTableAdapters.tbl_Hostel_SRegistrationTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudentId(Ds.Vw_StudentId_Card, StudentId)
        '  DaMess.FillByStudentId(Ds.tbl_Hostel_SRegistration, StudentId)

        'Dim Var_Mess_Count As Integer
        'Var_Mess_Count = 0
        'If Ds.tabletbl_Hostel_SRegistration.Rows.Count > 0 Then
        '    Var_Mess_Count = 1
        'End If

        DaCollege.Fill(Ds.Vw_College)

        '' CryAttendance.Subreports.Item("rptMessNo.rpt").SetDataSource(Ds.Tables.Item("tbl_Hostel_SRegistration"))
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Student Id Card"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        'CryAttendance.SetParameterValue("MC", Var_Mess_Count)
        'CryAttendance.SetParameterValue("UserName", UN)
        CryAttendance.SetParameterValue("Batch", Batch)

        EnableButtons(UserType)
        Dim printDoc As System.Drawing.Printing.PrintDocument
        printDoc = New System.Drawing.Printing.PrintDocument()
        ' CryAttendance.PrinterDuplex = crPRDPHorizontal
        If printDoc.PrinterSettings.CanDuplex = True Then
            printDoc.PrinterSettings.Duplex = System.Drawing.Printing.Duplex.Vertical
        End If

        If CryAttendance.PrintOptions.PrinterName.Contains("Zebra ZXP Series 3 USB Card Printer") Then
            CryAttendance.PrintOptions.PrinterName = "Zebra ZXP Series 3 USB Card Printer"
        End If
        'CryAttendance.PrintToPrinter(1, True, 1, 2)
        'frm.Show()
        'frm.Dispose()
        Return CryAttendance
    End Function

    Public Function StudentApplicationWithRollNo(ByVal StudentId As String) As rptStudentRegisterFormWithRollNo
        Dim Cry As New rptStudentRegisterFormWithRollNo
        Dim RepComm As String = ""
        RepComm = "BIO - DATA"
        Dim Ds As New Attendance
        Dim Da As New AttendanceTableAdapters.Vw_StudentSearchTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Dim DaPhoto As New AttendanceTableAdapters.tbl_fees_StudentPhotoTableAdapter

        Dim DaStudentAcademic As New AttendanceTableAdapters.tbl_fees_StudentAcademicTableAdapter

        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudenId(Ds.Vw_StudentSearch, StudentId)
        DaCollege.Fill(Ds.Vw_College)
        DaPhoto.FillByStudent(Ds.tbl_fees_StudentPhoto, StudentId)
        DaStudentAcademic.FillByStudent(Ds.tbl_fees_StudentAcademic, StudentId)
        Cry.Subreports.Item("rptLE.rpt").SetDataSource(Ds.Tables.Item("Vw_StudentSearch"))
        Cry.Subreports.Item("rptStudentRegAcademic.rpt").SetDataSource(Ds.Tables.Item("tbl_fees_StudentAcademic"))
        Cry.SummaryInfo.ReportComments = RepComm

        Cry.SetDataSource(DirectCast(Ds, DataSet))
        Return Cry

    End Function

    Public Sub AttendanceSlipWOG(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSlipGOB
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSlipGOBTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSlipGOB, TimeId)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FBConsolidate(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptFormCountConsolidate
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feedback Attendance Details"
        Dim Da As New AttendanceTableAdapters.tbl_fees_FormCountConsolidateTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_FormCountConsolidate)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Feedback Attendance Details"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub AttendanceRegisterDepartment(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String, ByVal Course As String, ByVal Section As String, ByVal StDt As Date, ByVal EndDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceRegister
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Attendance Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Endrdt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceRegisterTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Dim DaART As New AttendanceTableAdapters.Vw_ARTOTTableAdapter
        Dim DaARP As New AttendanceTableAdapters.Vw_ARPTableAdapter
        Dim DaARA As New AttendanceTableAdapters.Vw_ARATableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByDepartment(Ds.Vw_AttendanceRegister, Program, Course, Section, strdt, Endrdt)
        DaCollege.Fill(Ds.Vw_College)
        DaART.Fill(Ds.Vw_ARTOT)
        DaARP.Fill(Ds.Vw_ARP)
        DaARA.Fill(Ds.Vw_ARA)
        CryAttendance.Subreports.Item("rptART.rpt").SetDataSource(Ds.Tables.Item("Vw_ARTOT"))
        CryAttendance.Subreports.Item("rptARP.rpt").SetDataSource(Ds.Tables.Item("Vw_ARP"))
        CryAttendance.Subreports.Item("rptARA.rpt").SetDataSource(Ds.Tables.Item("Vw_ARA"))

        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Attendance Register Details"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        CryAttendance.SetParameterValue("EDate", Endrdt)
        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub AttendanceRegisterEmployee(ByVal UserType As Integer, ByVal UN As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal EmpCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceRegister
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Attendance Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Endrdt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceRegisterTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Dim DaART As New AttendanceTableAdapters.Vw_ARTOTTableAdapter
        Dim DaARP As New AttendanceTableAdapters.Vw_ARPTableAdapter
        Dim DaARA As New AttendanceTableAdapters.Vw_ARATableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmployee(Ds.Vw_AttendanceRegister, strdt, Endrdt, EmpCode)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm
        DaART.Fill(Ds.Vw_ARTOT)
        DaARP.Fill(Ds.Vw_ARP)
        DaARA.Fill(Ds.Vw_ARA)
        CryAttendance.Subreports.Item("rptART.rpt").SetDataSource(Ds.Tables.Item("Vw_ARTOT"))
        CryAttendance.Subreports.Item("rptARP.rpt").SetDataSource(Ds.Tables.Item("Vw_ARP"))
        CryAttendance.Subreports.Item("rptARA.rpt").SetDataSource(Ds.Tables.Item("Vw_ARA"))
        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Attendance Register Details"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        CryAttendance.SetParameterValue("EDate", Endrdt)
        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub AttendanceSlipWOGYF(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSlipGOBYF
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "List Of Attendance"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSlipGOBYFTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSlipGOBYF, TimeId)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Attendance"

        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    'Public Sub SubjectMaster(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
    '    frm.crViewer.ReportSource = Nothing
    '    frm.crViewer.Refresh()
    '    Dim CryAttendance As New rptSubject
    '    Dim Ds As New Attendance
    '    ' Dim DsAttendance As Attendance
    '    Dim RepComm As String = ""
    '    RepComm = "Program Details"
    '    Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
    '    Dim Da As New AttendanceTableAdapters.Vw_AttendanceSlipGOBTableAdapter
    '    Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
    '    Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
    '    Ds.EnforceConstraints = False
    '    DaLogo.Fill(Ds.Vw_Logo)
    '    Da.FillByTimeId(Ds.Vw_AttendanceSlipGOB, TimeId)
    '    DaCollege.Fill(Ds.Vw_College)
    '    CryAttendance.SummaryInfo.ReportComments = RepComm

    '    CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
    '    frm.Text = "List Of Programs"
    '    frm.crViewer.DisplayGroupTree = False
    '    frm.crViewer.ReportSource = CryAttendance

    '    CryAttendance.SetParameterValue("Date", strdt)
    '    EnableButtons(UserType)

    '    frm.Show()
    '    'frm.Dispose()
    '    ' Return Cry
    'End Sub

    Public Sub AttendanceSlipWG(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSlipGB
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSlipGBTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSlipGB, TimeId)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub AttendanceSlipWGYF(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSlipGBYF
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "List Attendance"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSlipGBYFTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSlipGBYF, TimeId)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Attendance"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub AttendanceSTWG(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSTGB
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSTGBTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSTGB, TimeId, strdt)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub AttendanceSTWGYF(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSTGBYF
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSTGBYFTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSTGBYF, TimeId, strdt)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub AttendanceSTWOG(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSTGOB
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSTGOBTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSTGOB, TimeId, strdt)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub AttendanceSTWOGYF(ByVal UserType As Integer, ByVal TimeId As String, ByVal StDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceSTGOBYF
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Program Details"
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New AttendanceTableAdapters.Vw_AttendanceSTGOBYFTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByTimeId(Ds.Vw_AttendanceSTGOBYF, TimeId, strdt)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "List Of Programs"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub



    Public Sub EmployeeReport(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptEmployeeDetails
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Employee Details"

        Dim Da As New EmployeeTableAdapters.Vw_EmployeeListTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_EmployeeList)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm

        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub ParameterReport(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptParameter
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Parameter Details"

        Dim Da As New EmployeeTableAdapters.tbl_fees_ParameterMasterTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_ParameterMaster)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm

        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Parameter Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub RatingReport(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptRating
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Rating Details"

        Dim Da As New EmployeeTableAdapters.tbl_fees_RatingMasterTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.tbl_fees_RatingMaster)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm

        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Rating Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub FeedbackReport(ByVal UserType As Integer, ByVal UN As String, ByVal StudentId As String, ByVal YearId As Integer, ByVal Semester As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFeedback
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feed back Details"

        Dim Da As New EmployeeTableAdapters.Vw_FeedbackTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudent(Ds.Vw_Feedback, StudentId, YearId, Semester)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee Details"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysis(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal YearId As Integer, ByVal Program As String, ByVal Course As String, ByVal Section As String, ByVal Semester As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysis
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feedback Analysis Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillBySM(Ds.Vw_FBAnalysis, Batch, YearId, Program, Course, Section, Semester)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Feedback Analysis Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisYS(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal YearId As Integer, ByVal Program As String, ByVal Semester As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysis
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feedback Analysis Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearSemester(Ds.Vw_FBAnalysis, Batch, YearId, Program, Semester)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Feedback Analysis Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisE(ByVal UserType As Integer, ByVal UN As String, ByVal EmpCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysis
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Employee Subject List Feedback Analysis Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmp(Ds.Vw_FBAnalysis, EmpCode)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee Subject List Feedback Analysis Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisSubject(ByVal UserType As Integer, ByVal UN As String, ByVal SubjectCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysis
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Subject Employee List Feedback Analysis Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillBySubject(Ds.Vw_FBAnalysis, SubjectCode)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Subject Employee List Feedback Analysis Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisResult(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal YearId As Integer, ByVal Program As String, ByVal Course As String, ByVal Result As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysis
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feedback Analysis Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByResult(Ds.Vw_FBAnalysis, Batch, YearId, Program, Course, Result)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Feedback Analysis Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisResultWOR(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal YearId As Integer, ByVal Program As String, ByVal Course As String, ByVal Result As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysisWOR
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Employee List Feedback Analysis Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByResult(Ds.Vw_FBAnalysis, Batch, YearId, Program, Course, Result)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee List Feedback Analysis Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub FeedbackAnalysisWOR(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal YearId As Integer, ByVal Program As String, ByVal Course As String, ByVal Section As String, ByVal Semester As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysisWOR
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Employee List Feedback Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillBySM(Ds.Vw_FBAnalysis, Batch, YearId, Program, Course, Section, Semester)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee List Feedback Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisWORYS(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal YearId As Integer, ByVal Program As String, ByVal Semester As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysisWOR
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Employee List Feedback Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByYearSemester(Ds.Vw_FBAnalysis, Batch, YearId, Program, Semester)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee List Feedback Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisWORE(ByVal UserType As Integer, ByVal UN As String, ByVal EmpCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysisWOR
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Employee Subject List Feedback Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmp(Ds.Vw_FBAnalysis, EmpCode)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Employee Subject List Feedback Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbackAnalysisWORSubject(ByVal UserType As Integer, ByVal UN As String, ByVal SubjectCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptFBAnalysisWOR
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Subject Employee List Feedback Report"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillBySubject(Ds.Vw_FBAnalysis, SubjectCode)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Subject Employee List Feedback Report"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbacExplanation(ByVal UserType As Integer, ByVal UN As String, ByVal Id As Integer, ByVal AY As String, ByVal DT As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptExplanation
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feedback Explanation"
        Dim strdt As Date = DT.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmployee(Ds.Vw_FBAnalysis, Id)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Feedback Explanation"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        CryEmp.SetParameterValue("AY", AY)
        CryEmp.SetParameterValue("DT", strdt)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FeedbacAppreciation(ByVal UserType As Integer, ByVal UN As String, ByVal Id As Integer, ByVal AY As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryEmp As New rptAppreciation
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Feedback Explanation"

        Dim Da As New EmployeeTableAdapters.Vw_FBAnalysisTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmployee(Ds.Vw_FBAnalysis, Id)
        DaCollege.Fill(Ds.Vw_College)
        CryEmp.SummaryInfo.ReportComments = RepComm
        Ds.EnforceConstraints = False
        CryEmp.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Feedback Explanation"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryEmp
        CryEmp.SetParameterValue("UserName", UN)
        CryEmp.SetParameterValue("AY", AY)
        EnableButtons(UserType)
        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub TimeTableReport(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptTimeTable
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Time Table"

        Dim Da As New AttendanceTableAdapters.Vw_TimeTableTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Dim DaMF As New AttendanceTableAdapters.Vw_TTMFTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_TimeTable)
        DaCollege.Fill(Ds.Vw_College)
        DaMF.Fill(Ds.Vw_TTMF)
        CryAttendance.Subreports.Item("rptTTMF.rpt").SetDataSource(Ds.Tables.Item("Vw_TTMF"))
        CryAttendance.SummaryInfo.ReportComments = RepComm
        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Time Table"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub EmployeeTimeTableReport(ByVal UserType As Integer, ByVal UN As String, ByVal EC As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptFacultyTimeTable
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim RepComm As String = ""
        RepComm = "Time Table"

        Dim Da As New AttendanceTableAdapters.Vw_TimeTableTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmployee(Ds.Vw_TimeTable, EC)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Time Table"
        frm.crViewer.DisplayGroupTree = True
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub AttendanceReport(ByVal UserType As Integer, ByVal Batch As String, ByVal Program As String, ByVal Year As Integer, ByVal Course As String, ByVal Section As String, ByVal StDt As Date, ByVal EndDt As Date, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceReport
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Student Attendance Report :" & strdt & " To " & strEndt & ""

        Dim Da As New AttendanceTableAdapters.Vw_AttendanceReportTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudents(Ds.Vw_AttendanceReport, Batch, Program, Year, Course, Section)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Student Attendance Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance




        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FacultyReport(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal Program As String, ByVal Year As Integer, ByVal Course As String, ByVal Semester As String, ByVal StDt As Date, ByVal EndDt As Date)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptFacultyAbsentees
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Faculty Absentees Report :" & strdt & " To " & strEndt & ""

        Dim Da As New EmployeeTableAdapters.Vw_FacultyAbsentTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByAbsentees(Ds.Vw_FacultyAbsent, Batch, Year, Program, Course, Semester, strdt, strEndt)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Faculty Absentees Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance


        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub FacultyAbsentReport(ByVal UserType As Integer, ByVal UN As String, ByVal EmpCode As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptFacultyAbsentees
        Dim Ds As New Employee
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Faculty Absent Report"

        Dim Da As New EmployeeTableAdapters.Vw_FacultyAbsentTableAdapter
        Dim DaLogo As New EmployeeTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New EmployeeTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByEmployee(Ds.Vw_FacultyAbsent, EmpCode)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Faculty Absentees Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance


        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub AttendanceReport(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal Program As String, ByVal Year As Integer, ByVal Course As String, ByVal Sem As String, ByVal Section As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAttendanceReportSem
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Student Attendance Report :" & Sem

        Dim Da As New AttendanceTableAdapters.Vw_AttendanceReportTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByStudents(Ds.Vw_AttendanceReport, Batch, Program, Year, Course, Section)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Student Attendance Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance


        CryAttendance.SetParameterValue("UserName", UN)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub CurrentIntakeStudent(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptCurrentIntakeStudent
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Current Intake Student"

        Dim Da As New AttendanceTableAdapters.Vw_Intake_ReportTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_Intake_Report)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Current Intake Student"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub IntakeReport(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptIntakeReport
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Intake Student Report"

        Dim Da As New AttendanceTableAdapters.Vw_Intake_ReportTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.Vw_Intake_Report)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Intake Student Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub IntakeFee(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptIntakeFee
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Intake Student Fee Details"

        Dim Da As New AttendanceTableAdapters.tbl_fees_IntakeAnalasisTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByProgram(Ds.tbl_fees_IntakeAnalasis, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Intake Student Fee Details"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub CurrentBatchesAI(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptAI
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Current Batches Active & In Active Students"

        Dim Da As New AttendanceTableAdapters.tbl_fees_IntakeAnalasisTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByProgram(Ds.tbl_fees_IntakeAnalasis, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Current Batches Active & In Active Students"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub BatchAnalysis(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptBatchAnalysis
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Batch Analysis Report"

        Dim Da As New AttendanceTableAdapters.tbl_fees_BatchAnalasisTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByProgram(Ds.tbl_fees_BatchAnalasis, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Batch Analysis Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Public Sub BatchDocuments(ByVal UserType As Integer, ByVal UN As String, ByVal Program As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptBatchDocuements
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance
        'Dim strdt As Date = StDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'Dim strEndt As Date = EndDt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        Dim RepComm As String = ""
        RepComm = "Batch Documents Report"

        Dim Da As New AttendanceTableAdapters.tbl_fees_BatchAnalasisTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByProgram(Ds.tbl_fees_BatchAnalasis, Program)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Batch Documents Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance
        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub


    Public Sub FSPercentageReport(ByVal UserType As Integer, ByVal UN As String, ByVal Batch As String, ByVal Program As String, ByVal Year As Integer, ByVal Course As String, ByVal Section As String, ByVal Semester As String, ByVal ArrSubject(,) As String, ByVal ArrSubjectNos(,) As String, ByVal PName As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rpt3FSPercentage
        Dim Ds As New Attendance

        Dim RepComm As String = ""
        RepComm = "Subject Wise Percentage Report"

        Dim Da As New AttendanceTableAdapters.Vw_FSPercentageTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.FillByBatch(Ds.Vw_FSPercentage, Batch, Program, Course, Section, Year, Semester)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Subject Wise Percentage Report"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance

        If Not ArrSubject Is Nothing Then
            If Not ArrSubjectNos Is Nothing Then
                For I = 0 To ArrSubject.Length - 2
                    CryAttendance.SetParameterValue("S" & I + 1, ArrSubject(0, I))

                Next

                For J = 0 To ArrSubjectNos.Length - 2
                    CryAttendance.SetParameterValue("SV" & J + 1, ArrSubjectNos(0, J))

                Next
            End If
        End If

        CryAttendance.SetParameterValue("Batch", Batch)
        CryAttendance.SetParameterValue("Year", Year)
        CryAttendance.SetParameterValue("Semester", Semester)
        CryAttendance.SetParameterValue("Program", Program)
        CryAttendance.SetParameterValue("Course", Course)
        CryAttendance.SetParameterValue("Section", Section)
        CryAttendance.SetParameterValue("PName", PName)






        EnableButtons(UserType)

        frm.Show()

    End Sub

    Public Sub FacultySubjectList(ByVal UserType As Integer, ByVal UN As String)
        frm.crViewer.ReportSource = Nothing
        frm.crViewer.Refresh()
        Dim CryAttendance As New rptFacultySubjectList
        Dim Ds As New Attendance
        ' Dim DsAttendance As Attendance

        Dim RepComm As String = ""
        RepComm = "Faculty Subject Report"
        Dim Da As New AttendanceTableAdapters.FacultySubjectTableAdapter
        Dim DaLogo As New AttendanceTableAdapters.Vw_LogoTableAdapter
        Dim DaCollege As New AttendanceTableAdapters.Vw_CollegeTableAdapter
        Ds.EnforceConstraints = False
        DaLogo.Fill(Ds.Vw_Logo)
        Da.Fill(Ds.FacultySubject)
        DaCollege.Fill(Ds.Vw_College)
        CryAttendance.SummaryInfo.ReportComments = RepComm

        CryAttendance.SetDataSource(DirectCast(Ds, DataSet))
        frm.Text = "Faculty Subject List"
        frm.crViewer.DisplayGroupTree = False
        frm.crViewer.ReportSource = CryAttendance


        CryAttendance.SetParameterValue("UserName", UN)


        'CryAttendance.SetParameterValue("Date", strdt)
        EnableButtons(UserType)

        frm.Show()
        'frm.Dispose()
        ' Return Cry
    End Sub

    Private Sub EnableButtons(ByVal UserType As Integer)
        If UserType = 3 Then

            frm.crViewer.EnableToolTips = False
            frm.crViewer.ShowPrintButton = False
            frm.crViewer.ShowExportButton = False

        End If
    End Sub

End Class
