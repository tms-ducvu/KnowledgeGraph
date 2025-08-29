$(function () {

    var l = abp.localization.getResource('KnowledgeGraph');

    // Use ABP's built-in API endpoint
    var dataTable = $('#ReviewsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: false, // Changed to false temporarily
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            processing: true,
            pageLength: 10,
            ajax: {
                url: '/api/app/review',
                type: 'GET',
                data: function (d) {
                    console.log('DataTables request:', d); // Debug log
                    // For client-side processing, we don't need to send parameters
                    return {};
                },
                dataSrc: function (json) {
                    console.log('API Response:', json); // Debug log

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
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('ReviewDeletionConfirmationMessage', data.record.reviewerName);
                                    },
                                    action: function (data) {
                                        // Gọi OnPostAsync method qua AJAX
                                        $.ajax({
                                            url: '/Reviews?handler=Delete',
                                            type: 'POST',
                                            data: { id: data.record.id },
                                            success: function (result) {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
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
                    title: l('Entity'),
                    data: "entityName",
                    render: function (data, type, row) {
                        return data || 'N/A';
                    }
                },
                {
                    title: l('ReviewerName'),
                    data: "reviewerName"
                },
                {
                    title: l('ReviewRating'),
                    data: "reviewRating",
                    render: function (data) {
                        var stars = '';
                        for (var i = 0; i < data; i++) {
                            stars += '⭐';
                        }
                        return stars;
                    }
                },
                {
                    title: l('ReviewReviewDate'),
                    data: "reviewReviewDate",
                    render: function (data) {
                        return luxon.DateTime.fromISO(data).toLocaleString(luxon.DateTime.DATE_SHORT);
                    }
                },
                {
                    title: l('ReviewContent'),
                    data: "reviewContent",
                    render: function (data) {
                        if (data && data.length > 50) {
                            return data.substring(0, 50) + '...';
                        }
                        return data || '';
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




    // Modal functionality for creating new review

    var createModal = new abp.ModalManager(abp.appPath + 'Reviews/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Reviews/EditModal');



    // Event handler for New Review button
    $(document).on('click', '#NewReviewButton', function (e) {
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
