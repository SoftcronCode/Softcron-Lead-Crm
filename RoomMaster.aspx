<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RoomMaster.aspx.cs" Inherits="DSERP_Client_UI.RoomMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <div class="pc-container">
        <div class="pc-content">
            <!-- Breadcrumb Section Start -->
            <div class="page-header">
                <div class="page-block">
                    <div class="row align-items-center">
                        <div class="col-md-12">
                            <ul class="breadcrumb">
                                <li class="breadcrumb-item"><a href="default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript: void(0)">Room Master</a></li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="page-header-title">
                                <h2 class="mb-0">Room Master</h2>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Breadcrumb Section End -->

            <!-- Add User Section Start -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header">
                            <h4>Room Master</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <div class="form-group col-lg-3">

                                    <label class="form-label">Room No. :</label>
                                    <asp:TextBox ID="TextBox_Room_No" name="Room_No" class="form-control" type="text" runat="server" placeholder="Enter Room No."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox_Room_No" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3">
                                    <label class="form-label">Room Type :</label>
                                    <asp:DropDownList ID="DropDownList_Room_Type" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList_Room_Type" ValidationGroup="Group1" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label class="form-label">Room Category :</label>
                                    <asp:DropDownList ID="DropDownList_Room_Category" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList_Room_Category" ValidationGroup="Group3" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3">
                                    <label class="form-label">Room Price :</label>
                                    <asp:TextBox ID="TextBox_Room_Price" name="Room_Price" class="form-control" type="text" runat="server" placeholder="Enter Room Price"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox_Room_Price" ValidationGroup="Group4" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label class="form-label">Room Capacity :</label>
                                    <asp:DropDownList ID="DropDownList_Room_Capacity" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList_Room_Capacity" ValidationGroup="Group5" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label class="form-label">Room Size :</label>
                                    <asp:DropDownList ID="DropDownList_Room_Size" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDownList_Room_Size" ValidationGroup="Group6" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                 <div class="form-group col-lg-3">
                                    <label class="form-label">Room Status :</label>
                                    <asp:DropDownList ID="DropDownList_Room_Status" Class="form-select" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList_Room_Status" ValidationGroup="Group6" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Select a value" ForeColor="Red" CssClass="absolute-position" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3">
                                    <label class="form-label">Floor Number :</label>
                                    <asp:TextBox ID="TextBox_Floor_Number" name="Floor_Number" class="form-control" type="text" runat="server" placeholder="Enter Floor Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox_Floor_Number" ValidationGroup="Group7" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Field is required" ForeColor="Red" CssClass="absolute-position"></asp:RequiredFieldValidator>
                                </div>
                                
                            </div>
                        </div>
                        <div class="card-footer bg-white border-0">
                            <asp:Button type="button" ID="ButtonSubmit" runat="server" CssClass="btn btn-light-primary me-2" Text="Add Room" ValidationGroup="Group1" OnClick="ButtonSubmit_Click" />
                           <%-- <asp:Button type="button" ID="ButtonUpdate" runat="server" CssClass="btn btn-light-primary me-2" Text="Update Booking" Visible="false" ValidationGroup="Group1" OnClick="ButtonUpdate_Click" />--%>
                            <asp:Button type="button" ID="ButtonClear" runat="server" CssClass="btn btn-light-secondary" Text="Clear" OnClick="ButtonClear_Click"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Add User Section End -->

                        <!-- DataTable Start -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="pb---20">
                                toggle column: <a class="toggle-vis toggle-anchor" data-column="0">room_master_tableID</a>
                                - <a class="toggle-vis toggle-anchor" data-column="1">room_no</a> 
                                - <a class="toggle-vis toggle-anchor" data-column="2">room_type</a>
                                - <a class="toggle-vis toggle-anchor" data-column="3">room_category</a>
                                - <a class="toggle-vis toggle-anchor" data-column="4">room_price</a>
                                - <a class="toggle-vis toggle-anchor" data-column="5">room_capacity</a>
                                - <a class="toggle-vis toggle-anchor" data-column="6">room_size</a>
                                - <a class="toggle-vis toggle-anchor" data-column="7">floor</a> 
                                - <a class="toggle-vis toggle-anchor" data-column="8">room_status</a> 
                                - <a class="toggle-vis toggle-anchor" data-column="9">status</a>
                            </div>
                            <div class="table-responsive dt-responsive">
                                <asp:gridview id="gridview" runat="server" cssclass="bookingdetail table table-striped table-bordered" width="100%" autogeneratecolumns="false"  datakeynames="room_master_tableID">
                                    <columns>
                                        <asp:boundfield datafield="room_master_tableID" headertext="room master tableID" sortexpression="room_master_tableID" />
                                        <asp:boundfield datafield="room_no" headertext="room no " sortexpression="room_no" />
                                        <asp:boundfield datafield="room_type"  headertext="room type " sortexpression="room_type" />
                                        <asp:boundfield datafield="room_category" headertext="room category " sortexpression="room_category" />
                                        <asp:boundfield datafield="room_price" headertext="room price" sortexpression="room_price" />
                                        <asp:boundfield datafield="room_capacity" headertext="room capacity" sortexpression="room_capacity" />
                                        <asp:boundfield datafield="room_size" headertext="room size" sortexpression="room_size" />
                                        <asp:boundfield datafield="floor" headertext="floor" sortexpression="floor" />
                                        <asp:boundfield datafield="room_status" headertext="room status" sortexpression="room_status" />
                                        <%--<asp:boundfield datafield="is_active" headertext="status" sortexpression="status" />--%>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                               
                                                <asp:Button ID="btnChangeStatus" runat="server" OnClick="btnChangeStatus_Click" Text='<%# Eval("is_active").ToString() == "True" ? "Active" : "De Activate"  %>' CssClass='<%# Eval("is_active").ToString() == "True" ? "status-active" : "status-deactive" %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:templatefield headertext="action">
                                            <itemtemplate>
                                                <asp:linkbutton id="btnviewreport" cssclass="me-2" runat="server" commandname="update" commandargument='<%# Eval("room_master_tableID")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="edit"><i class="fa-solid fa-pen-nib"></i></asp:linkbutton>                                      
                                                <asp:linkbutton id="button_invoice"  runat="server" commandname="delete" commandargument='<%# Eval("room_master_tableID")%>' data-bs-toggle="tooltip" data-bs-placement="bottom" title="delete"><i class="fa-solid fa-trash-can"></i></asp:linkbutton>
                                            </itemtemplate>
                                        </asp:templatefield>
                                    </columns>
                                </asp:gridview>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- datatable end -->


        </div>
    </div>


</asp:Content>
