$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport:
            {
                read: function (e) {
                    $.ajax({
                        url: '/Magazine/GetMagazines',
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
                        url: '/Magazine/MagazineEdit',
                        type: "POST",
                        dataType: "json",
                        data: {
                            magazineViewModel: opt.data.models[0]
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
                        url: '/Magazine/DeleteMagazine/' + options.data.models[0].Id,
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
                        url: '/Magazine/AddMagazine',
                        type: "POST",
                        dataType: "json",
                        data: {
                            magazineViewModel: options.data.models[0]
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
                    Name: { validation: { required: true } },
                    Number: { validation: { required: true } },
                    YearOfPublication: { validation: { required: true } }
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
            { field: "Number", title: "Number" },
            { field: "YearOfPublication", title: "Year Of Publication" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "popup"
    });
});
