$(function () {

    var l = abp.localization.getResource('KnowledgeGraph');

    // Use ABP's built-in API endpoint
    var dataTable = $('#EntitiesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: false, // Changed to false temporarily
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            processing: true,
            pageLength: 10,
            ajax: {
                url: '/api/app/entity',
                type: 'GET',
                data: function (d) {
                    console.log('DataTables request:', d);
                    // For client-side processing, we don't need to send parameters
                    return {};
                },
                dataSrc: function (json) {
                    console.log('API Response:', json);

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
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: function (data) {
                                        try {
                                            var rawValue = data.entityIsActive;
                                            var isActive = (rawValue === true || rawValue === "true" || rawValue === 1 || rawValue === "1");
                                            return isActive ? l('Deactivate') : l('Activate');
                                        } catch (e) {

                                            return l('ToggleActive'); // Fallback
                                        }
                                    },
                                    action: function (data) {
                                        $.ajax({
                                            url: '/Entities?handler=ToggleActive',
                                            type: 'POST',
                                            data: { id: data.record.id },
                                            success: function (result) {
                                                if (result.success) {
                                                    abp.notify.info(result.message || l('SuccessfullyToggled'));
                                                    dataTable.ajax.reload();
                                                } else {
                                                    abp.notify.error(result.message || 'Toggle failed');
                                                }
                                            },
                                            error: function (xhr, status, error) {
                                                abp.notify.error('Toggle failed: ' + error);
                                            }
                                        });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('EntityDeletionConfirmationMessage', data.entityName);
                                    },
                                    action: function (data) {
                                        $.ajax({
                                            url: '/Entities?handler=Delete',
                                            type: 'POST',
                                            data: { id: data.id },
                                            success: function (result) {
                                                if (result.success) {
                                                    abp.notify.info(result.message || l('SuccessfullyDeleted'));
                                                    dataTable.ajax.reload();
                                                } else {
                                                    abp.notify.error(result.message || 'Delete failed');
                                                }
                                            },
                                            error: function (xhr, status, error) {
                                                console.error('Delete error:', error);
                                                abp.notify.error('Delete failed: ' + error);
                                            }
                                        });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('EntityName'),
                    data: "entityName"
                },
                {
                    title: l('EntityCode'),
                    data: "entityCode"
                },
                {
                    title: l('EntityBusinessType'),
                    data: "entityBusinessType"
                },
                {
                    title: l('EntityPhone'),
                    data: "entityPhone"
                },
                {
                    title: l('EntityEmail'),
                    data: "entityEmail"
                },
                {
                    title: l('EntityIsActive'),
                    data: "entityIsActive",
                    render: function (data) {
                        return data ? '<span class="badge bg-success">Active</span>' : '<span class="badge bg-secondary">Inactive</span>';
                    }
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

    // Modal functionality for creating new entity
    var createModal = new abp.ModalManager(abp.appPath + 'Entities/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Entities/EditModal');

    // Event handler for New Entity button
    $(document).on('click', '#NewEntityButton', function (e) {
        e.preventDefault();

        if (createModal && typeof createModal.open === 'function') {
            createModal.open();
        } else {
            console.error('createModal is not properly initialized!');
            alert('Modal is not ready. Please refresh the page.');
        }
    });

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

});
