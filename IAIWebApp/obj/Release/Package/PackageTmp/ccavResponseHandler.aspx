<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ccavResponseHandler.aspx.cs" Inherits="IAIWebApp.ccavResponseHandler" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>I AM Interviewed</title>
    <link rel="icon" href="assets/OuterCSS/img/favicon.png" type="image/x-icon" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900i" rel="stylesheet">
    <link href="assets/InnerCSS/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="assets/InnerCSS/global/plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/fullcalendar/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/jqvmap/jqvmap/jqvmap.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="assets/InnerCSS/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="assets/InnerCSS/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="assets/InnerCSS/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="assets/InnerCSS/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/InnerCSS/global/css/custom.css" rel="stylesheet" />
    <link href="assets/InnerCSS/global/css/Loader_Notifications.css" rel="stylesheet" />
    <link href="assets/InnerCSS/global/css/ViewRating.css" rel="stylesheet" />
    <link href="assets/InnerCSS/global/plugins/datepicker/bootstrap-datepicker.css" rel="stylesheet">
    <link href="assets/InnerCSS/global/css/customCompany.css" rel="stylesheet" />
    <!-- END THEME LAYOUT STYLES -->
</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white">

    <div class="page-wrapper">
        <!-- BEGIN HEADER -->
        <div class="page-header navbar navbar-fixed-top">
            <!-- BEGIN HEADER INNER -->
            <div class="page-header-inner ">
                <!-- BEGIN LOGO -->
                <div class="page-logo">
                    <a href="index.html">
                        <img src="assets/InnerCSS/layouts/layout/img/IAmInterviewedLogo.png" alt="logo" class="logo-default search-rating-logo" />
                    </a>
                </div>
                <!-- END LOGO -->
                <!-- BEGIN RESPONSIVE MENU TOGGLER -->
                <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse">
                    <span></span>
                </a>
                <!-- END RESPONSIVE MENU TOGGLER -->
                <!-- BEGIN TOP NAVIGATION MENU -->
                <div class="top-menu">
                    <ul class="nav navbar-nav pull-right">
                        <!-- BEGIN USER LOGIN DROPDOWN -->
                        <!-- DOC: Apply "dropdown-dark" class after below "dropdown-extended" to change the dropdown styte -->
                        <li>
                            <a href="signIn.html">
                                <i class="icon-user"></i>
                                <span class="username username-hide-on-mobile">sign In </span>
                            </a>
                        </li>
                        <!-- END USER LOGIN DROPDOWN -->
                        <!-- BEGIN QUICK SIDEBAR TOGGLER -->
                        <!-- DOC: Apply "dropdown-dark" class after below "dropdown-extended" to change the dropdown styte -->
                        <!-- END QUICK SIDEBAR TOGGLER -->
                    </ul>
                </div>
                <!-- END TOP NAVIGATION MENU -->
            </div>
            <!-- END HEADER INNER -->
        </div>
        <!-- END HEADER -->
        <!-- BEGIN HEADER & CONTENT DIVIDER -->
        <div class="clearfix"></div>
        <!-- END HEADER & CONTENT DIVIDER -->
        <!-- BEGIN CONTAINER -->
        <div class="page-container">
            <!-- BEGIN CONTENT -->
            <div class="page-content-wrapper">
                <!-- BEGIN CONTENT BODY -->
                <div class="page-content page-main--right-content" style="margin-left: 0px;">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divsuccess" runat="server" class="alert alert-success alert-dismissible" role="alert" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <strong>Success!</strong>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>
                            <div id="divError" runat="server" class="alert alert-danger alert-dismissible" role="alert" visible="false">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <strong>Warning!</strong>
                                <asp:Label ID="lblerrormess" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <a href="Candidate.html">Please click here to go back to dashboard</a>
                        </div>
                    </div>
                </div>
                <!-- END CONTENT BODY -->
            </div>
            <!-- END CONTENT -->
            <!-- BEGIN QUICK SIDEBAR -->
            <!-- END QUICK SIDEBAR -->
        </div>
        <!-- END CONTAINER -->
        <!-- BEGIN FOOTER -->
        <div class="page-footer">
            <div class="page-footer-inner">
                2019 &copy; I Am Interviewed
            </div>
            <div class="scroll-to-top">
                <i class="icon-arrow-up"></i>
            </div>
        </div>
        <!-- END FOOTER -->
    </div>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <!--[if lt IE 9]>
    <script src="assets/InnerCSS/global/plugins/respond.min.js"></script>
    <script src="assets/InnerCSS/global/plugins/excanvas.min.js"></script>
    <script src="assets/InnerCSS/global/plugins/ie8.fix.min.js"></script>
    <![endif]-->
    <!-- BEGIN CORE PLUGINS -->
    <script src="assets/InnerCSS/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/InnerCSS/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/morris/morris.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/TwitterHover/twitter-bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/morris/raphael-min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/fullcalendar/fullcalendar.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/horizontal-timeline/horizontal-timeline.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jquery.sparkline.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/jquery.vmap.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="assets/InnerCSS/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="assets/InnerCSS/pages/scripts/dashboard.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/pages/scripts/form-wizard.min.js" type="text/javascript"></script>
    <!--<script src="assets/InnerCSS/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>-->
    <script src="assets/InnerCSS/global/plugins/datepicker/bootstrap-datepicker.js"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <script src="assets/InnerCSS/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/layouts/layout/scripts/demo.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
    <script src="assets/InnerCSS/global/scripts/custom.js"></script>
</body>
</html>
