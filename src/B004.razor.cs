using MetaFrm.Extensions;
using MetaFrm.Razor.DataGrid;
using MetaFrm.Razor.Group;
using MetaFrm.Service;
using MetaFrm.Web.Bootstrap;
using Microsoft.AspNetCore.Components.Web;
using MetaFrm.Management.Razor.Models;
using MetaFrm.Management.Razor.ViewModels;
using MetaFrm.Database;

namespace MetaFrm.Management.Razor
{
    /// <summary>
    /// B004
    /// </summary>
    public partial class B004
    {
        #region Variable
        internal B004ViewModel B004ViewModel { get; set; } = Factory.CreateViewModel<B004ViewModel>();

        internal DataGridControl<AccountModel>? DataGridControl;
        internal List<ColumnDefinitions>? ColumnDefinitions;

        internal IEnumerable<Data.DataRow>? ResponsibilityItems;

        internal AccountModel SelectItem = new();

        internal GroupWindowStatus GroupWindowStatus = GroupWindowStatus.Close;
        #endregion


        #region Init
        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override void OnInitialized()
        {
            if (this.ColumnDefinitions == null)
            {
                this.ColumnDefinitions = new();
                this.ColumnDefinitions.AddRange(new ColumnDefinitions[] {
                    new ColumnDefinitions{ DataField = nameof(AccountModel.NICKNAME), Caption = "Nickname", DataType = DbType.NVarChar, Class = "text-break", SortDirection = SortDirection.Ascending },
                    new ColumnDefinitions{ DataField = nameof(AccountModel.EMAIL), Caption = "Email", DataType = DbType.NVarChar, Class = "text-break", SortDirection = SortDirection.Normal },
                    new ColumnDefinitions{ DataField = nameof(AccountModel.RESPONSIBILITY_NAME), Caption = "Permissions", DataType = DbType.NVarChar, Class = "text-break", SortDirection = SortDirection.Normal }});
            }
        }

        /// <summary>
        /// OnAfterRenderAsync
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (!this.IsLogin())
                    this.Navigation?.NavigateTo("/", true);

                this.B004ViewModel = await this.GetSession<B004ViewModel>(nameof(this.B004ViewModel));

                this.Search();

                this.StateHasChanged();
            }
        }
        #endregion


        #region Dictionary
        private void OnResultEventResponsibilityItems(IEnumerable<Data.DataRow> dataRows) => this.ResponsibilityItems = dataRows;
        #endregion


        #region IO
        private void OnSearch()
        {
            if (this.DataGridControl != null)
                this.DataGridControl.CurrentPageNumber = 1;

            this.Search();
        }
        private void Search()
        {
            Response response;

            try
            {
                if (this.B004ViewModel.IsBusy) return;

                this.B004ViewModel.IsBusy = true;

                ServiceData serviceData = new()
                {
                    Token = this.UserClaim("Token")
                };
                serviceData["1"].CommandText = this.GetAttribute("Search");
                serviceData["1"].AddParameter(nameof(this.B004ViewModel.SearchModel.SEARCH_TEXT), DbType.NVarChar, 4000, this.B004ViewModel.SearchModel.SEARCH_TEXT);
                serviceData["1"].AddParameter("USER_ID", DbType.Int, 3, this.UserClaim("Account.USER_ID").ToInt());
                serviceData["1"].AddParameter("PAGE_NO", DbType.Int, 3, this.DataGridControl != null ? this.DataGridControl.CurrentPageNumber : 1);
                serviceData["1"].AddParameter("PAGE_SIZE", DbType.Int, 3, this.DataGridControl != null && this.DataGridControl.PagingEnabled ? this.DataGridControl.PagingSize : int.MaxValue);

                response = serviceData.ServiceRequest(serviceData);

                if (response.Status == Status.OK)
                {
                    this.B004ViewModel.SelectResultModel.Clear();

                    if (response.DataSet != null && response.DataSet.DataTables.Count > 0)
                    {
                        foreach (var datarow in response.DataSet.DataTables[0].DataRows)
                        {
                            this.B004ViewModel.SelectResultModel.Add(new AccountModel
                            {
                                USER_ID = datarow.Int(nameof(AccountModel.USER_ID)),
                                EMAIL = datarow.String(nameof(AccountModel.EMAIL)),
                                NICKNAME = datarow.String(nameof(AccountModel.NICKNAME)),
                                FULLNAME = datarow.String(nameof(AccountModel.FULLNAME)),
                                PHONENUMBER = datarow.String(nameof(AccountModel.PHONENUMBER)),
                                RESPONSIBILITY_ID = datarow.Int(nameof(AccountModel.RESPONSIBILITY_ID)),
                                RESPONSIBILITY_NAME = datarow.String(nameof(AccountModel.RESPONSIBILITY_NAME)),
                                INACTIVE_DATE = datarow.DateTime(nameof(AccountModel.INACTIVE_DATE)),
                            });
                        }
                    }
                }
                else
                {
                    if (response.Message != null)
                    {
                        this.ModalShow("Warning", response.Message, new() { { "Ok", Btn.Warning } }, null);
                    }
                }
            }
            catch (Exception e)
            {
                this.ModalShow("An Exception has occurred.", e.Message, new() { { "Ok", Btn.Danger } }, null);
            }
            finally
            {
                this.B004ViewModel.IsBusy = false;
#pragma warning disable CS4014 // 이 호출을 대기하지 않으므로 호출이 완료되기 전에 현재 메서드가 계속 실행됩니다.
                this.SetSession(nameof(B004ViewModel), this.B004ViewModel);
#pragma warning restore CS4014 // 이 호출을 대기하지 않으므로 호출이 완료되기 전에 현재 메서드가 계속 실행됩니다.
            }
        }

        private void Save()
        {
            Response? response;
            string? value;

            response = null;

            try
            {
                if (this.B004ViewModel.IsBusy)
                    return;

                if (this.SelectItem == null)
                    return;

                this.B004ViewModel.IsBusy = true;

                ServiceData serviceData = new()
                {
                    TransactionScope = true,
                    Token = this.UserClaim("Token")
                };
                serviceData["1"].CommandText = this.GetAttribute("Save");
                serviceData["1"].AddParameter($"{nameof(this.SelectItem.USER_ID)}_KEY", DbType.Int, 3, this.SelectItem.USER_ID);
                serviceData["1"].AddParameter(nameof(this.SelectItem.EMAIL), DbType.NVarChar, 100, this.SelectItem.EMAIL);
                serviceData["1"].AddParameter(nameof(this.SelectItem.NICKNAME), DbType.NVarChar, 50, this.SelectItem.NICKNAME);
                serviceData["1"].AddParameter(nameof(this.SelectItem.FULLNAME), DbType.NVarChar, 200, this.SelectItem.FULLNAME);
                serviceData["1"].AddParameter(nameof(this.SelectItem.PHONENUMBER), DbType.NVarChar, 50, this.SelectItem.PHONENUMBER);
                serviceData["1"].AddParameter(nameof(this.SelectItem.RESPONSIBILITY_ID), DbType.Int, 3, this.SelectItem.RESPONSIBILITY_ID);
                serviceData["1"].AddParameter(nameof(this.SelectItem.INACTIVE_DATE), DbType.DateTime, 0, this.SelectItem.INACTIVE_DATE);
                serviceData["1"].AddParameter("USER_ID", DbType.Int, 3, this.UserClaim("Account.USER_ID").ToInt());

                response = serviceData.ServiceRequest(serviceData);

                if (response.Status == Status.OK)
                {
                    if (response.DataSet != null && response.DataSet.DataTables.Count > 0 && response.DataSet.DataTables[0].DataRows.Count > 0 && this.SelectItem != null && this.SelectItem.RESPONSIBILITY_ID == null)
                    {
                        value = response.DataSet.DataTables[0].DataRows[0].String("Value");

                        if (value != null)
                            this.SelectItem.RESPONSIBILITY_ID = value.ToInt();
                    }

                    this.ToastShow("Completed", $"{this.GetAttribute("Title")} registered successfully.", Alert.ToastDuration.Long);
                }
                else
                {
                    if (response.Message != null)
                    {
                        this.ModalShow("Warning", response.Message, new() { { "Ok", Btn.Warning } }, null);
                    }
                }
            }
            catch (Exception e)
            {
                this.ModalShow("An Exception has occurred.", e.Message, new() { { "Ok", Btn.Danger } }, null);
            }
            finally
            {
                this.B004ViewModel.IsBusy = false;

                if (response != null && response.Status == Status.OK)
                {
                    this.Search();
                    this.StateHasChanged();
                }
            }
        }
        #endregion


        #region Event
        private void SearchKeydown(KeyboardEventArgs args)
        {
            if (args.Key == "Enter")
            {
                this.OnSearch();
            }
        }

        private void RowModify(AccountModel item)
        {
            this.SelectItem = new()
            {
                USER_ID = item.USER_ID,
                EMAIL = item.EMAIL,
                NICKNAME = item.NICKNAME,
                FULLNAME = item.FULLNAME,
                PHONENUMBER = item.PHONENUMBER,
                RESPONSIBILITY_ID = item.RESPONSIBILITY_ID,
                RESPONSIBILITY_NAME = item.RESPONSIBILITY_NAME,
                INACTIVE_DATE = item.INACTIVE_DATE,
            };

            this.GroupWindowStatus = GroupWindowStatus.Maximize;
        }

        private void Close()
        {
            this.GroupWindowStatus = GroupWindowStatus.Close;
        }
        #endregion
    }
}