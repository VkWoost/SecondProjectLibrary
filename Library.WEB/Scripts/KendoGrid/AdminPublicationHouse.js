$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport:
            {
                read: function (e) {
                    $.ajax({
                        url: '/PublicationHouse/GetPublicationHouses',
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
                        url: '/PublicationHouse/PublicationHouseEdit',
                        type: "POST",
                        dataType: "json",
                        data: {
                            publicationHouseViewModel: opt.data.models[0]
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
                        url: '/PublicationHouse/DeletePublicationHouse/' + options.data.models[0].Id,
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
                        url: '/PublicationHouse/AddPublicationHouse',
                        type: "POST",
                        dataType: "json",
                        data: {
                            publicationHouseViewModel: options.data.models[0]
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
                    Adress: { validation: { required: true } }
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
            { field: "Adress", title: "Adress" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "popup",
        edit: function (e) {
            if (e.model.isNew()) {
                $(".k-window-title")[0].innerHTML = "Add";
                $(".k-button.k-button-icontext.k-primary.k-grid-update")[0].textContent = "Add";
            }
        }
    });
});
