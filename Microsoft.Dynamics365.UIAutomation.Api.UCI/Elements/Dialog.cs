﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using Microsoft.Dynamics365.UIAutomation.Api.UCI.DTO;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;

namespace Microsoft.Dynamics365.UIAutomation.Api.UCI
{
    public class Dialogs : Element
    {
        private readonly WebClient _client;

        public Dialogs(WebClient client) : base()
        {
            _client = client;
        }

        public enum AssignTo
        {
            Me,
            User,
            Team
        }

        /// <summary>
        /// Assigns a record to a user or team
        /// </summary>
        /// <param name="to">Enum used to assign record to user or team</param>
        /// <param name="userOrTeamName">Name of the user or team to assign to</param>
        public void Assign(AssignTo to, string userOrTeamName = null)
        {
            _client.AssignDialog(to, userOrTeamName);
        }

        /// <summary>
        /// Clicks the Close button if true, or clicks Cancel if false
        /// </summary>
        /// <param name="closeOrCancel"></param>
        public void CloseActivity(bool closeOrCancel)
        {
            _client.CloseActivity(closeOrCancel);
        }

        /// <summary>
        /// Clicks Close As Won or Close As Loss on Opportunity Close dialog
        /// </summary>
        /// <param name="closeAsWon"></param>
        public void CloseOpportunity(bool closeAsWon)
        {
            _client.CloseOpportunity(closeAsWon);
        }

        /// <summary>
        /// Enters the values provided and closes an opportunity
        /// </summary>
        /// <param name="revenue">Value for Revenue field</param>
        /// <param name="closeDate">Value for Close Date field</param>
        /// <param name="description">Value for Description field</param>
        public void CloseOpportunity(double revenue, DateTime closeDate, string description)
        {
            _client.CloseOpportunity(revenue, closeDate, description);
        }

        /// <summary>
        /// Closes the warning dialog during login
        /// </summary>
        /// <returns></returns>
        public bool CloseWarningDialog()
        {
            return _client.CloseWarningDialog();
        }

        /// <summary>
        /// Closes a Modal Dialog
        /// </summary>
        /// <returns></returns>
        public bool CloseModalDialog()
        {
            return _client.CloseModalDialog();
        }

        /// <summary>
        /// Clicks OK or Cancel on the confirmation dialog.  true = OK, false = Cancel
        /// </summary>
        /// <param name="clickConfirmButton"></param>
        /// <returns></returns>
        public bool ConfirmationDialog(bool clickConfirmButton)
        {
            return _client.ConfirmationDialog(clickConfirmButton);
        }

        /// <summary>
        /// Clicks OK or Cancel on the rejection dialog.  true = OK, false = Cancel
        /// </summary>
        /// <param name="clickOkButton"></param>
        /// <param name="rejectionReason"></param>
        /// <param name="otherReason"></param>
        /// <returns></returns>
        public bool RejectionDialog(bool clickOkButton, string rejectionReason=null, string otherReason=null)
        {
            return _client.RejectionDialog(clickOkButton, rejectionReason, otherReason);
        }

        /// <summary>
        /// Enters user or team and reason on rejection dialog.  true = OK, false = Cancel
        /// </summary>
        /// <param name="clickOkButton"></param>
        /// <param name="userOrTeam"></param>
        /// <param name="reassignReason"></param>
        /// <returns></returns>
        public bool ReassignDialog(bool clickOkButton, string userOrTeam, string reassignReason = null)
        {
            return _client.ReassignDialog(clickOkButton, userOrTeam, reassignReason);
        }

        /// <summary>
        /// Clicks OK or Cancel on the confirm completion dialog.  true = OK, false = Cancel
        /// </summary>
        /// <param name="clickOkButton"></param>
        /// <param name="reason"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        public bool ConfirmCompletionDialog(bool clickOkButton, string reason = null, string comments = null)
        {
            return _client.RejectionDialog(clickOkButton, reason, comments);
        }

        /// <summary>
        /// Clicks OK or Cancel on the Run Report dialog.  true = OK, false = Cancel
        /// </summary>
        /// <param name="clickOkButton"></param>
        /// <returns></returns>
        public bool ReportDialog(bool clickOkButton)
        {
            return _client.ReportDialog(clickOkButton);
        }

        /// <summary>
        /// Clicks 'Ignore And Save' or 'Cancel' on the Duplicate Detection dialog.  true = Ignore And Save, false = Cancel
        /// </summary>
        /// <param name="clickConfirmButton"></param>
        /// <returns></returns>
        public bool DuplicateDetection(bool clickSaveOrCancel)
        {
            return _client.DuplicateDetection(clickSaveOrCancel);
        }

        /// <summary>
        /// Clicks Confirm or Cancel on the Publish dialog.  true = Confirm, false = Cancel
        /// </summary>
        /// <param name="clickOkButton"></param>
        /// <returns></returns>
        public bool PublishDialog(bool clickConfirmButton)
        {
            return _client.PublishDialog(clickConfirmButton);
        }

        /// <summary>
        /// Sets the value of a field
        /// </summary>
        /// <param name="field">The field</param>
        /// <param name="value">The value</param>
        public void SetValue(string field, string value)
        {
            _client.SetValue(field, value, FormContextType.Dialog);
        }

        /// <summary>
        /// Sets the value of a Lookup.
        /// </summary>
        /// <param name="control">The lookup field name, value or index of the lookup.</param>
        public void SetValue(LookupItem control, bool useJsScroll = false)
        {
            _client.SetValue(control, FormContextType.Dialog, useJsScroll);
        }

        /// <summary>
        /// Sets the value of an ActivityParty Lookup.
        /// </summary>
        /// <param name="controls">The activityparty lookup field name, value or index of the lookup.</param>
        /// <example>xrmApp.Entity.SetValue(new LookupItem[] { new LookupItem { Name = "to", Value = "A. Datum Corporation (sample)" } });</example>
        public void SetValue(LookupItem[] controls)
        {
            _client.SetValue(controls, FormContextType.Dialog);
        }

        /// <summary>
        /// Sets the value of a picklist or status field.
        /// </summary>
        /// <param name="option">The option you want to set.</param>
        public void SetValue(OptionSet optionSet)
        {
            _client.SetValue(optionSet, FormContextType.Dialog);
        }

        /// <summary>
        /// Sets the value of a Boolean Item.
        /// </summary>
        /// <param name="option">The boolean field name.</param>
        public void SetValue(BooleanItem option)
        {
            _client.SetValue(option, FormContextType.Dialog);
        }

        /// <summary>
        /// Sets the value of a Date Field.
        /// </summary>
        /// <param name="field">Date field name.</param>
        /// <param name="date">DateTime value.</param>
        /// <param name="formatDate">Datetime format matching Short Date formatting personal options.</param>
        /// <param name="formatTime">Datetime format matching Short Time formatting personal options.</param>
        /// <example>xrmApp.Dialog.SetValue("birthdate", DateTime.Parse("11/1/1980"));</example>
        /// <example>xrmApp.Dialog.SetValue("new_actualclosedatetime", DateTime.Now, "MM/dd/yyyy", "hh:mm tt");</example>
        /// <example>xrmApp.Dialog.SetValue("estimatedclosedate", DateTime.Now);</example>
        public void SetValue(string field, DateTime date, string formatDate = null, string formatTime = null)
        {
            _client.SetValue(field, date, FormContextType.Dialog, formatDate, formatTime);
        }

        /// <summary>
        /// Sets the value of a Date Field.
        /// </summary>
        /// <param name="control">Date field control.</param>
        public void SetValue(DateTimeControl control)
        {
            _client.SetValue(control, FormContextType.Dialog);
        }

        /// <summary>
        /// Sets/Removes the value from the multselect type control
        /// </summary>
        /// <param name="option">Object of type MultiValueOptionSet containing name of the Field and the values to be set/removed</param>
        /// <param name="removeExistingValues">False - Values will be set. True - Values will be removed</param>
        public void SetValue(MultiValueOptionSet option, bool removeExistingValues = false)
        {
            _client.SetValue(option, FormContextType.Dialog, removeExistingValues);
        }

        /// <summary>
        /// Clicks OK or Cancel on the Set State (Activate / Deactivate) dialog.  true = OK, false = Cancel
        /// </summary>
        /// <param name="clickOkButton"></param>
        /// <returns></returns>
        public bool SetStateDialog(bool clickOkButton)
        {
            return _client.SetStateDialog(clickOkButton);
        }

        /// <summary>
        /// Clicks OK on Error dialog
        /// </summary>
        public void CloseErrorDialog()
        {
            _client.CloseErrorDialog();
        }
    }
}
