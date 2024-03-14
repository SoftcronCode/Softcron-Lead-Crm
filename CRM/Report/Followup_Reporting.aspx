<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Followup_Reporting.aspx.cs" Inherits="DSERP_Client_UI.Followup_Reporting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pc-container">
        <div class="pc-content">
            <h1 class="page-heading">Follow Up Reporting</h1>
            <!-- DataTable Start -->
            <div class="row ">
                <div class="col-sm-6">
                    <div class="table-responsive dt-responsive">
                        <asp:Repeater ID="NotificationRepeater" runat="server">
                            <ItemTemplate>
                                <div class="card mb-2">
                                    <div class="card-body">
                                        <div class="d-flex">
                                            <div class="flex-grow-1">
                                                <span class="float-end text-sm text-muted" data-bs-toggle="tooltip" data-bs-placement="top" title='<%# Eval("followup_date") %>' style="cursor: pointer;">
                                                    <%# CalculateTimeDifference(Eval("followup_date")) %>
                                                </span>
                                                <h5 class="text-body mb-2 capitalize-text">New Reminder For <%# Eval("first_name") %></h5>
                                                <p class="mb-0 capitalize-text">- <%# Eval("followup_text") %></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <!-- DataTable End -->
        </div>
    </div>

</asp:Content>



<%--Content PLaceHoler For Javascript--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
