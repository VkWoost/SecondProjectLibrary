﻿$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport:
            {
                read: function (e) {
                    $.ajax({
                        url: '/Author/GetAuthors',
                        type: "GET",
                        dataType: "json",
                        success: function (result) {
                            e.success(result);
                        },
                        error: function (result) {
                            e.error(result);
                        }
                    });
                },
            },
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, nullable: true },
                    Name: { validation: { required: true } }
                }
            }
        }
    });
    $("#grid").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        height: 550,
        columns: [
            { field: "Name", title: "Name" },
        ]
    });
});
