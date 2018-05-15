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
                update: function (opt) {
                    $.ajax({
                        url: '/Author/AuthorEdit',
                        type: "POST",
                        dataType: "json",
                        data: {
                            authorViewModel: opt.data.models[0]
                        },
                        success: function (result) {
                            opt.success(result);
                        },
                        error: function (result) {
                            opt.error(result);
                        },
                    });
                },
                destroy: function (options) {
                    $.ajax({
                        url: '/Author/DeleteAuthor/' + options.data.models[0].Id,
                        type: "POST",
                        dataType: "json",
                        success: function (result) {
                            options.success(result);
                        },
                        error: function (result) {
                            options.error(result);
                        }
                    });
                },
                create: function (options) {
                    $.ajax({
                        url: '/Author/AddAuthor',
                        type: "POST",
                        dataType: "json",
                        data: {
                            authorViewModel: options.data.models[0]
                        },
                        success: function (result) {
                            options.success(result);
                        },
                        error: function (result) {
                            options.error(result);
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
        toolbar: ["create"],
        columns: [
            { field: "Name", title: "Name" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "popup"
    });
});