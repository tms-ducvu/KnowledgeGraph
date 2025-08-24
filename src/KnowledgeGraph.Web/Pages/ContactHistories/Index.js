$(function () {
    var l = abp.localization.getResource('KnowledgeGraph');

    var viewModal = new abp.ModalManager(abp.appPath + 'ContactHistories/ViewModal');

    var dataTable = $('#ContactHistoriesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: false, // Changed to false temporarily, similar to Contacts
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            processing: true, // Added, similar to Contacts
            pageLength: 10,   // Added, similar to Contacts
            ajax: {
                url: '/api/app/contact-history', // Corrected API endpoint
                type: 'GET',
                data: function (d) {
                    console.log('DataTables request (ContactHistories):', d); // Debug log
                    // For client-side processing, we don't need to send parameters
                    return {};
                },
                dataSrc: function (json) {
                    console.log('API Response (ContactHistories):', json); // Debug log

                    // Set total records for pagination
                    if (json && typeof json.totalCount !== 'undefined') {
                        // Force DataTables to recognize the total count
                        setTimeout(function () {
                            dataTable.page.len(10).draw();
                            dataTable.page.info().recordsTotal = json.totalCount;
                            dataTable.page.info().recordsDisplay = json.items ? json.items.length : 0;
                        }, 100);
                    }

                    return json.items || [];
                }
            },
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('View'),
                                    action: function (data) {
                                        viewModal.open({ id: data.record.id });
                                    }
                                },
                                                                {
                                    text: l('Sync'),
                                    confirmMessage: function (data) {
                                        return l('ContactHistorySyncConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        console.log('Sync clicked for contact history:', data.record);
                                        console.log('Calling Razor Page handler:', '/ContactHistories?handler=Sync for id=' + data.record.id);

                                        // Call Razor Page handler for sync
                                        $.ajax({
                                            url: '/ContactHistories?handler=Sync',
                                            type: 'POST',
                                            data: { id: data.record.id },
                                            headers: {
                                                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                                            },
                                            success: function (result) {
                                                console.log('Sync success:', result);
                                                if (result.success) {
                                                    abp.notify.success(l('SyncSuccess'));
                                                    // Reload DataTable to show updated sync status
                                                    dataTable.ajax.reload();
                                                } else {
                                                    abp.notify.error(result.message || l('SyncFailed'));
                                                }
                                            },
                                            error: function (xhr, status, error) {
                                                console.error('Sync error:', error);
                                                abp.notify.error(l('SyncFailed') + ': ' + error);
                                            }
                                        });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('ContactName'),
                    data: "name"
                },
                {
                    title: l('Email'),
                    data: "email"
                },
                {
                    title: l('PhoneNumber'),
                    data: "phoneNumber"
                },
                {
                    title: l('AddressCity'),
                    data: "addressCity"
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        return luxon.DateTime.fromISO(data).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

});
