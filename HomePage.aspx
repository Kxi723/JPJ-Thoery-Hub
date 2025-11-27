<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="JPJ_Theory_Hub.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Hero Section -->
    <div class="text-center mb-5">
        <h1 class="display-4 fw-bold mb-4" style="color: #202020;">DRIVING LICENSE EXAM TEST MALAYSIA - PRACTICE</h1>
    </div>

    <!-- Main Content Card -->
    <div class="row mb-5">
        <div class="col-md-6 mb-4">
            <div class="liquid-glass h-100">
                <h2 class="fw-bold mb-3" style="color: #202020;">Why would you like to get a driving license in Malaysia?</h2>
                <p style="font-size: 16px; line-height: 1.8; color: #333;">
                    There could be many reasons why you would like to obtain a driving license in Malaysia. 
                    Perhaps you are a resident of Malaysia and need a license to legally operate a vehicle, or 
                    maybe you are an international student or worker who needs a Malaysian driving license to 
                    get around.
                </p>
            </div>
        </div>
        <div class="col-md-6 mb-4">
            <div class="liquid-glass h-100">
                <h2 class="fw-bold mb-3" style="color: #202020;">How to effectively learn for the theory exam?</h2>
                <p style="font-size: 16px; line-height: 1.8; color: #333;">
                    The best way to learn is to practice with real questions. On the official exam often happens 
                    that translation to English is not that good or just that a question is tricky. By practising with 
                    real questions you will learn the way how questions work, and what language is used and 
                    you will be able to memorise tricky ones.
                </p>
            </div>
        </div>
    </div>

    <!-- License Types Section -->
    <div class="mb-5">
        <h2 class="text-center fw-bold mb-4" style="color: #202020;">Why would you like to get a driving license in Malaysia?</h2>
        <div class="liquid-glass">
            <p style="font-size: 16px; line-height: 1.8; color: #333; margin-bottom: 20px;">
                In Malaysia, there are a few different types of driving licenses that you can obtain, depending on your 
                qualifications and driving experience. The types of licenses include:
            </p>
            <div class="row text-center mb-3">
                <div class="col-md-4">
                    <div class="p-3">
                        <h4 class="fw-bold" style="color: #ff9b36;">Probationary Driving License (PDL)</h4>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="p-3">
                        <h4 class="fw-bold" style="color: #ff9b36;">Full Driving License (FDL)</h4>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="p-3">
                        <h4 class="fw-bold" style="color: #ff9b36;">Competent Driving License (CDL)</h4>
                    </div>
                </div>
            </div>
            <p style="font-size: 16px; line-height: 1.8; color: #333;">
                A PDL is issued to new drivers who have passed their basic driving theory test and practical driving test. 
                It is a provisional license that allows the holder to drive with additional restrictions. After the probationary 
                period, PDL holders can apply for an FDL if they have not committed any major traffic offences. An FDL is a 
                full driving license that allows the holder to drive without any restrictions or limitations. A CDL, on the other 
                hand, is issued to commercial vehicle drivers, such as lorry or bus drivers, who have completed additional 
                training and tests.
            </p>
        </div>
    </div>

    <!-- KPP Test and Practical Test Section -->
    <div class="row mb-5">
        <div class="col-md-6 mb-4">
            <div class="liquid-glass h-100">
                <h3 class="fw-bold mb-3" style="color: #202020;">KPP Test</h3>
                <p style="font-size: 16px; line-height: 1.8; color: #333;">
                    The KPP (Kursus Pendidikan Pemandu) test is a mandatory theory test that you must pass 
                    before you can apply for a Malaysian driving license. The test is designed to assess your 
                    knowledge of traffic rules and regulations, safe driving practices, and road signs. The test 
                    consists of 50 multiple-choice questions, and you must answer at least 42 questions correctly 
                    to pass. You can take the KPP test at any of the Road Transport Department (JPJ) branches or 
                    at any authorized driving institutes.
                </p>
            </div>
        </div>
        <div class="col-md-6 mb-4">
            <div class="liquid-glass h-100">
                <h3 class="fw-bold mb-3" style="color: #202020;">Practical Driving Test</h3>
                <p style="font-size: 16px; line-height: 1.8; color: #333;">
                    Once you have passed the KPP test, you can begin taking practical driving lessons with a 
                    licensed driving instructor. You will need to complete a minimum number of driving lessons 
                    (typically between 16 and 24 hours) before you can take the practical driving test. The test is 
                    designed to assess your driving skills and ability to follow traffic rules and regulations. You will 
                    need to demonstrate your ability to perform a range of driving maneuvers, such as parking, 
                    reversing, and changing lanes.
                </p>
            </div>
        </div>
    </div>

    <!-- Good Luck Message -->
    <div class="text-center mb-5">
        <h3 class="fw-bold" style="color: #202020;">I wish you luck and hope you will be able to get your driving license easily!</h3>
    </div>

    <!-- Our Team Section -->
    <div class="mb-5">
        <h2 class="text-center fw-bold mb-4" style="color: #202020;">Our Team</h2>
        <div class="row">
            <div class="col-md-3 mb-4">
                <div class="liquid-glass text-center">
                    <div class="mb-3">
                        <asp:Image ID="imgJason" runat="server" ImageUrl="~/Image/Jason.png" 
                            AlternateText="Jason Lai Kwang Xi" 
                            style="width: 120px; height: 120px; border-radius: 50%; object-fit: cover; border: 3px solid rgba(255, 155, 54, 0.5);" />
                    </div>
                    <h5 class="fw-bold mb-2" style="color: #202020;">Jason Lai Kwang Xi</h5>
                    <p class="mb-0" style="color: #666; font-size: 18px;">TP080522</p>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="liquid-glass text-center">
                    <div class="mb-3">
                        <asp:Image ID="imgKahHeng" runat="server" ImageUrl="~/Image/KahHeng.png" 
                            AlternateText="Chong Kah Heng" 
                            style="width: 120px; height: 120px; border-radius: 50%; object-fit: cover; border: 3px solid rgba(255, 155, 54, 0.5);" />
                    </div>
                    <h5 class="fw-bold mb-2" style="color: #202020;">Chong Kah Heng</h5>
                    <p class="mb-0" style="color: #666; font-size: 18px;">TP080938</p>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="liquid-glass text-center">
                    <div class="mb-3">
                        <asp:Image ID="imgTianSheng" runat="server" ImageUrl="~/Image/TianSheng.png" 
                            AlternateText="Chen Tiansheng" 
                            style="width: 120px; height: 120px; border-radius: 50%; object-fit: cover; border: 3px solid rgba(255, 155, 54, 0.5);" />
                    </div>
                    <h5 class="fw-bold mb-2" style="color: #202020;">Chen Tiansheng</h5>
                    <p class="mb-0" style="color: #666; font-size: 18px;">TP073854</p>
                </div>
            </div>
            <div class="col-md-3 mb-4">
                <div class="liquid-glass text-center">
                    <div class="mb-3">
                        <asp:Image ID="imgYungCee" runat="server" ImageUrl="~/Image/YungCee.png" 
                            AlternateText="Lee Yung Cee" 
                            style="width: 120px; height: 120px; border-radius: 50%; object-fit: cover; border: 3px solid rgba(255, 155, 54, 0.5);" />
                    </div>
                    <h5 class="fw-bold mb-2" style="color: #202020;">Lee Yung Cee</h5>
                    <p class="mb-0" style="color: #666; font-size: 18px;">TP080674</p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>