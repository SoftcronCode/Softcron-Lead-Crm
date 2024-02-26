<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LeadReport.aspx.cs" Inherits="DSERP_Client_UI.Lead_Reporting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pc-container">
        <div class="pc-content">
            <!-- DataTable Start -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                <!-- Define your column toggle links here -->
                            </div>
                            <div class="table-responsive dt-responsive">
                                    <asp:GridView ID="gridview" runat="server" CssClass="bookingdetail table table-striped table-bordered" Width="100%" AutoGenerateColumns="false" DataKeyNames="lead_id" OnRowCommand="Button_GridViewCommand" OnRowUpdating="gridview_RowUpdating" OnRowDeleting="GridView_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="lead_id" HeaderText="Lead ID" />
                                        <asp:BoundField DataField="customer_type" HeaderText="Customer Type" />
                                        <asp:BoundField DataField="client_name" HeaderText="Client Name" />
                                        <asp:BoundField DataField="email" HeaderText="Email" />
                                        <asp:BoundField DataField="phone" HeaderText="Phone" />
                                        <asp:BoundField DataField="product_name" HeaderText="Product Name" />
                                        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="source" HeaderText="Source" />
                                        <asp:BoundField DataField="is_active" HeaderText="Status" />
                                        <asp:BoundField DataField="created_date" HeaderText="Created Date" />

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnviewreport" CssClass="me-2" runat="server" CommandName="update" CommandArgument='<%# Eval("lead_id")%>' ToolTip="Edit" OnClientClick="return toggleEditDiv();">
                                                       <i class="fa-solid fa-pen-nib"></i>
                                                </asp:LinkButton>
                                               
                                                   <asp:LinkButton ID="deletebutton" CssClass="me-2" runat="server" CommandName="delete" CommandArgument='<%# Eval("lead_id")%>' ToolTip="Delete"> 
                                                        <i class="fa-solid fa-trash-can"></i>
                                                   </asp:LinkButton>

                                                <asp:LinkButton ID="BtnFollowup"  runat="server" CommandName="Followup" CommandArgument='<%# Eval("lead_id")%>' ToolTip="FollowUp">
                                                      <i class="fa-solid fa-check-double"></i> 
                                                </asp:LinkButton>


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- datatable end -->

              <%------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
    <%------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

    <%-- follow up details --%>
    <div class="modal right fade" id="followup_details" tabindex="-1" role="dialog" aria-modal="true" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="row w-100">
                        <div class="col-md-7 account d-flex">
                            <div>
                              <p class="mb-0" style="font-size: 18px; font-weight: bold; margin-top: 0px;">Lead FollowUp</p>

                            </div>
                        </div>
                    </div>
                    <asp:Button ID="Button1" class="btn-close" runat="server" OnClick="button_Close" aria-label="Close" />
                </div>
                <div class="modal-body">



                    <%-- timeline code   --%>
                    <style>
                        .cursor-pointer {
                            cursor: pointer;
                        }
                    </style>

                    <div class="chat-container">
                        <div class="box">
                            <ul id="first-list">

                                <asp:Repeater ID="ChatRepeater" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <span></span>
                                            <div class="info"><%# Eval("followup_text") %></div>
                                            <div class="time">
                                                <span data-bs-toggle="tooltip" data-bs-placement="top" title='<%# Eval("followup_date") %>' class="cursor-pointer">
                                                    <%# CalculateTimeDifference(Eval("followup_date")) %>
                                                </span>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>


                                <script type="text/javascript">
                                    document.addEventListener('DOMContentLoaded', function () {
                                        var tooltips = new bootstrap.Tooltip(document.body, {
                                            selector: '[data-bs-toggle="tooltip"]'
                                        });
                                    });
                                </script>


                            </ul>
                        </div>
                    </div>

                    <hr />

                    <%-- follow up text fields  --%>
                    <div class="row remark">

                        <div class="col-md-6">
                            <span class="fw-bold">Remark</span>
                            <%--<textarea id="Remark" runat="server" class="form-control mb-2" rows="5"></textarea>--%>
                            <asp:TextBox ID="Remark_txt" runat="server" CssClass="form-control mb-2" Rows="5"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <span class="fw-bold">Next Follow Up </span>
                            <asp:TextBox ID="followUpDateTime" runat="server" CssClass="form-control mb-2" Type="datetime-local"></asp:TextBox>

                        </div>
                        <br />

                        <div class="col-md-4 mt-2 m-l-5 ">
                            <asp:Button ID="Button_add_followup" runat="server" CssClass="border-0 btn btn-primary btn-gradient-primary btn-rounded" Text="Save" OnClick="Btn_AddFollowup" />
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>


    <%------------------------------------------------------------------------------------------------------------------------------------------------------------------%>
    <%------------------------------------------------------------------------------------------------------------------------------------------------------------------%>

           
        </div>
        
    </div>

    
</asp:Content>

