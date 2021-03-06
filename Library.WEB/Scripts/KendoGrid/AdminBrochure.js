﻿$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport:
            {
                read: function (e) {
                    $.ajax({
                        url: '/Brochure/GetBrochures',
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
                        url: '/Brochure/BrochureEdit',
                        type: "POST",
                        dataType: "json",
                        data: {
                            brochureViewModel: opt.data.models[0]
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
                        url: '/Brochure/DeleteBrochure/' + options.data.models[0].Id,
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
                        url: '/Brochure/AddBrochure',
                        type: "POST",
                        dataType: "json",
                        data: {
                            brochureViewModel: options.data.models[0]
                        },
                        success: function (result) {
                            options.success(result);
                        },
                        error: function (result) {
                            options.error(result);
                        }
                    });
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "Id",
                fields: {
                    Id: { editable: false, nullable: true },
                    Name: { validation: { required: true } },
                    TypeOfCover: { validation: { required: true } },
                    NumberOfPages: { validation: { required: true } }
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
            { field: "TypeOfCover", title: "Type Of Cover" },
            { field: "NumberOfPages", title: "Number Of Pages" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "popup",
        edit: function(e) {
            if (e.model.isNew()) {
                $(".k-window-title")[0].innerHTML = "Add";
                $(".k-button.k-button-icontext.k-primary.k-grid-update")[0].textContent = "Add";
            }
        }
    });
});
