using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicalOperationSearch.Resource;
using LogicalOperationSearch.Dao;
using LogicalOperationSearch.Db;
using HistorySearchCondition;
using LogicalOperationSearch.Data;
using LogicalOperationSearch.Search;
using LogicalOperationSearch.Entity;
using LogicalOperationSearch.Handler;

namespace LogicalOperationSearch
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Simple search selected flag.
        /// </summary>
        private bool IsSimpleSearchSelect = true;

        /// <summary>
        /// Search Target
        /// </summary>
        private SearchTargets SearchTarget;

        /// <summary>
        /// Search type
        /// </summary>
        private SearchTypes SearchType;

        /// <summary>
        /// Search background process
        /// </summary>
        BackgroundWorker searchBackgroundWoker = new BackgroundWorker();

        public frmMain()
        {
            InitializeComponent();

            // Database Init
            DatabaseManager.ThreadInstance = DatabaseManager.CreateInstance();
            DatabaseManager.ThreadInstance.BeginTransaction();

            // Selected default index for combobox.
            cbSearchType.SelectedIndex = 0;
            //btnSearch.Enabled = false;

            UpdateHistorySearchList();

            flpSearchConditionCreatorPanel.ControlAdded += new ControlEventHandler(OnControlChanges);
            flpSearchConditionCreatorPanel.ControlRemoved += new ControlEventHandler(OnControlChanges);

            // Add first item into Search Operator Panel without operator
            DropPanelControl usrCtrl = new DropPanelControl();
            usrCtrl.IsDisplayOperatorSelection = false;
            usrCtrl.OnDeleteLinkClicked += new EventHandler(OnDeleteLinkClicked);
            flpSearchConditionCreatorPanel.Controls.Add(usrCtrl);

            #region Initialize searching background worker.
            // Report process status
            searchBackgroundWoker.WorkerReportsProgress = true;

            // Handlers
            searchBackgroundWoker.DoWork += ExecuteSearchByThreadInstance;

            // Report process status.
            searchBackgroundWoker.ProgressChanged += UpdateSearchProcessReport;

            // Report end status.
            searchBackgroundWoker.RunWorkerCompleted += ExecuteSearchByThreadInstanceComplete;

            #endregion Initialize searching background worker.
        }

        #region Search Process Worker
        // Execute search function.
        private void ExecuteSearchByThreadInstance(object sender, DoWorkEventArgs args)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                if (tbSimpleSearch.SelectedIndex == 0)
                {
                    // Simple search
                    // Collection search information.
                    // Search target.
                    if (rdMathematics.Checked)
                    {
                        SearchTarget = SearchTargets.Mathematics;
                    }
                    else if (rdPhysical.Checked)
                    {
                        SearchTarget = SearchTargets.Physical;
                    }
                    else
                    {
                        SearchTarget = SearchTargets.Sciencetifics;
                    }

                    // Search condition.
                    string searchCondition = string.Empty;
                    DateTime? fromDate = null;
                    DateTime? toDate = null;
                    bool validate = true;

                    switch (SearchType)
                    {
                        case SearchTypes.Author:
                        case SearchTypes.BookName:
                        case SearchTypes.Barcode:
                            if (string.IsNullOrEmpty(tbSearchText.Text.Trim()))
                            {
                                MessageBox.Show("Please input required fields", "Missing input", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                validate = false;
                            }
                            else
                            {
                                searchCondition = tbSearchText.Text.Trim();
                            }
                            break;
                        case SearchTypes.PulishDate:
                            fromDate = dtFrom.Value;
                            toDate = dtTo.Value;
                            break;

                        default:
                            throw new Exception("Selected Search Type Is Invalid.");
                    }

                    if (validate)
                    {

                        // Generate information search.
                        SearchConditionInformation info = new SearchConditionInformation()
                        {
                            SearchTarget = SearchTarget,
                            SearchType = SearchType,
                            TextCondition = searchCondition,
                            FromDate = fromDate,
                            ToDate = toDate
                        };

                        // Update search process.
                        pgbSearchProcess.Style = ProgressBarStyle.Marquee;
                        pgbSearchProcess.MarqueeAnimationSpeed = 50;

                        // Search and set resource for result list.
                        SearchHandler handler = new SearchHandler();
                        dgDisplayResult.DataSource = handler.SearchBook(info);

                        // Call function to update flowlayoutpanel
                        UpdateHistorySearchList();
                    }
                }
                else
                {
                    using (DatabaseManager localDatabase = DatabaseManager.CreateInstance())
                    {
                        // Update search process.
                        pgbSearchProcess.Style = ProgressBarStyle.Marquee;
                        pgbSearchProcess.MarqueeAnimationSpeed = 50;

                        // Advance search.
                        CustomGroupSearchCondition customGroupSearchCondition = CollectGroupSearchInfos();
                        SearchWithOperator(customGroupSearchCondition);
                    }

                }
            });
        }

        // Update process status.
        private void UpdateSearchProcessReport(object sender, ProgressChangedEventArgs args)
        {
        }

        // Searching completed event.
        private void ExecuteSearchByThreadInstanceComplete(object sender, RunWorkerCompletedEventArgs args)
        {
            // Stop search process and show complete message.
            pgbSearchProcess.Style = ProgressBarStyle.Continuous;
            pgbSearchProcess.MarqueeAnimationSpeed = 0;

            MessageBox.Show("Search completed !");
        }
        #endregion Search Process Worker

        private void OnControlChanges(Object sender, ControlEventArgs args)
        {
            if (flpSearchConditionCreatorPanel.Controls.Count > 0)
            {
                ((DropPanelControl)flpSearchConditionCreatorPanel.Controls[0]).IsDisplayOperatorSelection = false;
            }
        }

        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbSearchType = sender as ComboBox;
            SearchType = (SearchTypes)cbSearchType.SelectedIndex;

            switch (SearchType)
            {
                case SearchTypes.Author:
                case SearchTypes.BookName:
                case SearchTypes.Barcode:
                    this.pnSearchDateTime.Visible = false;
                    this.pnSearchText.Visible = true;
                    this.SearchTarget = SearchTargets.Barcode;
                    break;

                case SearchTypes.PulishDate:
                    pnSearchText.Visible = false;
                    pnSearchDateTime.Visible = true;
                    break;

                default:
                    throw new Exception("Selected Search Type Is Invalid.");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl searchTabControl = sender as TabControl;

            IsSimpleSearchSelect =
                searchTabControl.SelectedIndex == 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (searchBackgroundWoker != null)
            {
                searchBackgroundWoker.RunWorkerAsync();
            }
        }

        private void UpdateHistorySearchList()
        {
            // Clear history panel first.
            flpHistorySearch.Controls.Clear();

            // Update search history list.
            List<HistorySearchEntity> histories =
                new HistorySearchDao().GetHistorySearch();

            if (histories.Count != 0)
            {
                foreach (HistorySearchEntity entity in histories)
                {
                    HistorySearchControl historyCtrl = new HistorySearchControl()
                    {
                        HistoryId = entity.SearchHistoryId,
                        SearchTarget = (SearchTargets)entity.SearchTargetTable,
                        SearchType = (SearchTypes)entity.SearchTargetColumn,
                        SearchContent = entity.SearchCondition,
                        FromDate = entity.FromDate,
                        ToDate = entity.ToDate
                    };

                    historyCtrl.OnDeleteButtonClicked += new EventHandler(OnHistoryItemDeleteButtonClicked);
                    flpHistorySearch.Controls.Add(historyCtrl);
                }
            }
        }

        #region Delegate Event
        /// <summary>
        /// Delete link of operator search selection panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteLinkClicked(object sender, EventArgs e)
        {
            if (flpSearchConditionCreatorPanel.Controls.Count <= 1)
            {
                // Can not remove last item.
                MessageBox.Show("Can not remove last item from the list.",
                    "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                // Handle event from here
                var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                         "Confirm Delete!!",
                                         MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    DropPanelControl ctrl = sender as DropPanelControl;
                    flpSearchConditionCreatorPanel.Controls.Remove(ctrl);
                }
            }

        }

        /// <summary>
        /// Delete history search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHistoryItemDeleteButtonClicked(object sender, EventArgs e)
        {
            // Handle event from here
            var confirmResult = MessageBox.Show("Are you sure to delete this item ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                HistorySearchControl ctrl = sender as HistorySearchControl;

                // Delete from database.
                using (DatabaseManager localDatabaseManager = DatabaseManager.CreateInstance())
                {
                    localDatabaseManager.BeginTransaction();

                    try
                    {
                        localDatabaseManager.Update(
                              string.Format(@"UPDATE
                                search_history
                              SET 
                                delete_flag = TRUE
                              WHERE
                                search_history_id = {0}", ctrl.HistoryId));

                        localDatabaseManager.Commit();

                        flpHistorySearch.Controls.Remove(ctrl);
                    }
                    catch (Exception ex)
                    {
                        // Rollback
                        localDatabaseManager.Rollback();

                        MessageBox.Show(string.Format("Error during delete history search item. Error = [{0}]", ex.Message),
                            "Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                
            }
        }
        #endregion Delegate Event

        private void tbSearchText_TextChanged(object sender, EventArgs e)
        {
            if (SearchType == SearchTypes.Author
                || SearchType == SearchTypes.Barcode
                || SearchType == SearchTypes.BookName)
            {
                btnSearch.Enabled = !string.IsNullOrEmpty(tbSearchText.Text.Trim());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DropPanelControl usrCtrl = new DropPanelControl();
            usrCtrl.OnDeleteLinkClicked += new EventHandler(OnDeleteLinkClicked);
            usrCtrl.IsDisplayOperatorSelection = true;
            flpSearchConditionCreatorPanel.Controls.Add(usrCtrl);
        }

        /// <summary>
        /// Collect group search condition from screen.
        /// </summary>
        /// <returns></returns>
        private CustomGroupSearchCondition CollectGroupSearchInfos ()
        {
            CustomGroupSearchCondition groupConditions = new CustomGroupSearchCondition();

            // Check items on flpDropPanel.
            if (flpSearchConditionCreatorPanel.Controls.Count == 0)
            {
                MessageBox.Show("Please set custom search before execute search function.!",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                List<CustomSearchCondition> searchConditions = new List<CustomSearchCondition>();

                foreach (DropPanelControl item in flpSearchConditionCreatorPanel.Controls)
                { 
                    List<HistorySearchData> SearchConditions = new List<HistorySearchData>();

                    foreach (HistorySearchControl historyItem in item.HistorySearchControl)
                    {
                        SearchConditions.Add(
                            new HistorySearchData()
                            {
                                HistoryId = historyItem.HistoryId,
                                SearchType = (int)historyItem.SearchType,
                                SearchTarget = (int)historyItem.SearchTarget,
                                TextSearchCondition = historyItem.SearchContent,
                                FromDate = historyItem.FromDate,
                                ToDate = historyItem.ToDate
                            });
                    }

                    searchConditions.Add(
                        new CustomSearchCondition()
                        {
                            Operator = item.IsDisplayOperatorSelection ? item.Operator : LogicalOperator.NONE,
                            SearchConditionList = SearchConditions
                        });
                }

                groupConditions.CustomSearchConditionList = searchConditions;
            }

            return groupConditions;
        }

        /// <summary>
        /// Search with operator search
        /// </summary>
        /// <param name="groupSearchCondition"></param>
        private void SearchWithOperator(CustomGroupSearchCondition groupSearchCondition)
        {
            List<AbstractSearchFilter> filters = new List<AbstractSearchFilter>();
            if (groupSearchCondition.CustomSearchConditionList.Count > 0)
            {
                foreach (CustomSearchCondition condition in groupSearchCondition.CustomSearchConditionList)
                {
                    AbstractSearchFilter filter = CreateSearchFilterFromCustomSearch(condition.SearchConditionList);

                    if (filter != null)
                    {
                        filters.Add(filter);
                    }

                    if (condition.Operator != LogicalOperator.NONE)
                    {
                        AbstractSearchFilter operatorFilter = 
                            DetermineOperatorSearch(filters, condition.Operator);

                        // Clear filter list.
                        filters = new List<AbstractSearchFilter>();
                        filters.Add(operatorFilter);
                    }
                }
            }

            if (filters.Count != 0)
            {
                // Start to search.
                SearchFilterResult result = filters[0].Search(new SearchFilterResult());

                dgDisplayResult.DataSource = new List<BookEntity>();
        
                if (result.HitCount != 0)
                {
                    // Insert result into datagridview.
                    dgDisplayResult.DataSource = new BookDao().GetBookFromSearchResult(result.TableName);
                }
                
            }
        }

        /// <summary>
        /// Create search filter from custom search.
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private AbstractSearchFilter CreateSearchFilterFromCustomSearch (List<HistorySearchData> conditions)
        {
            List<AbstractSearchFilter> filters = new List<AbstractSearchFilter>();

            foreach (HistorySearchData condition in conditions)
            {
                SearchConditionInformation conditionDetail = new SearchConditionInformation()
                {
                    SearchTarget = (SearchTargets)condition.SearchTarget,
                    SearchType = (SearchTypes)condition.SearchType,
                    TextCondition = condition.TextSearchCondition,
                    FromDate = condition.FromDate,
                    ToDate = condition.ToDate
                };

                filters.Add(SearchHandler.CreateSearchFilter(conditionDetail));
            }

            // Determine search filter type.
            if (filters.Count > 1)
            {
                return new OrOperatorSearch(filters);
            }
            else if (filters.Count == 1)
            {
                return filters[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determine based on search operator type
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="operatorType"></param>
        /// <returns></returns>
        private AbstractSearchFilter DetermineOperatorSearch (List<AbstractSearchFilter> filters, LogicalOperator operatorType)
        {
            switch (operatorType)
            {
                case LogicalOperator.AND:
                    return new AndSearchOperator(filters);
                case LogicalOperator.OR:
                    return new OrOperatorSearch(filters);
                case LogicalOperator.NOT:
                    return new NotOperatorSearch(filters);
                default:
                    throw new Exception("Search operator is not valid");
            }
        }
    }
}
