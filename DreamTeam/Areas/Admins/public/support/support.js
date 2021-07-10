//message send when done with success background, fail with danger background
const sendMessage = (messageText, doneORfail) => {
    let message = $("#message");
    if (doneORfail == "done") {
        message.addClass("alert-success");
        message.removeClass("alert-danger");
        $("#message-text").text(messageText);
    } else {
        message.addClass("alert-danger");
        message.removeClass("alert-success");
        $("#message-text").text(messageText);
    }
    message.css({
        visibility: "visible",
    });
    setTimeout(() => {
        message.css({
            visibility: "hidden",
        });
    }, 4000);
};


function createTable(data, arrClName, arrClDataName, btnsave, btndetails, btndelete) {
    let html = `
      <table class="table col">
        <thead class="thead-light">
          <tr>
            <th scope="col">#</th>`;
    arrClName.forEach((item) => {
        html += `<th scope="col">${item}</th>`;
    });
    html += `
            <th scope="col">Tác Vụ</th>
          </tr>
        </thead>
        <tbody>`

    data.forEach((item, index) => {
        html += `
          <tr>
            <th scope="row">${++index}</th>`

        arrClDataName.forEach(cldata_item => {
            html += setData(item[cldata_item[0]], cldata_item[1], cldata_item[0], item.Id, cldata_item[2])
        })

        html += `<td>`
        if (btnsave) {
            html += `
              <button
                value="${item.Id}"
                type="button"
                class="btn btn-primary btn_save"
                style="margin: 5px"
              >
                <i class="fa fa-check"></i>
              </button>`
        }
        if (btndetails) {
            html += `
              <button
                value="${item.Id}"
                type="button"
                class="btn btn-success btn_detail"
                style="margin: 5px"
              >
                <i class="fa fa-pen"></i>
              </button>`
        }
        if (btndelete) {
            html += `
              <button
              value="${item.Id}"
                type="button"
                class="btn btn-danger btn_delete"
                style="margin: 5px"
              >
                <i class="fa fa-trash"></i>
              </button>`
        }
        html += `</td>
          </tr>`
    });

    html += `
          </tbody>
        </table>
        `;
    return html;

}

// type 1 in binh thuong [columnName, type]
// type 2 in button true false [columnName,type]
// type 3 in input [columnName,type]
// type 4 in 1 select di kem voi 1 list cho truoc [columnName,type,list]
function setData(value, type, clDataName, id, list) {
    switch (type) {
        case 1: {
            if (value == "") {
                value = "Trống"
            }
            return `<td class=" ${clDataName} ${id}">${value}</td>`
        } case 2: {
            let color = ''
            if (value == true) {
                color = 'success'
            } else {
                color = 'secondary'
            }
            return `<td><a href='' value='${id}' style="padding:5px; width:80px;" class="badge badge-${color} ${clDataName} ${id} ">${value}</a></td>`
        } case 3: {
            return `<td><input type="text" style="text-align: center;" class="form-control ${clDataName} ${id}" value="${value}"></td>`
        } case 4: {
            let html = `<td>
        <select value="${id}" class="form-control ${clDataName} ${id}">`
            list.forEach((item, index) => {
                let selected = ""
                if (item.Name == value) {
                    selected = 'selected'
                }
                html += `
        <option class="form-control" ${selected} value="${item.Id}">(${++index}) ${item.Name}</option>
        `
            });
            html += `</select>
      </td>`
            return html;
        }
    }
}

function changeBackgroundBoolOne(element) {
    if (element.text() == "true") {
        element.removeClass("badge-success");
        element.addClass("badge-secondary");
        element.text("false");
    } else {
        element.addClass("badge-success");
        element.removeClass("badge-secondary");
        element.text("true");
    }
}

function changeBackgroundBoolMany(element,className) {
    $(`.${className}`).removeClass("badge-success");
    $(`.${className}`).addClass("badge-secondary");
    $(`.${className}`).text("false");
    element.addClass("badge-success");
    element.removeClass("badge-secondary");
    element.text("true");
}

function putOne(id, data,route) {
    return $.ajax({
        url: route + id,
        method: "PUT",
        data: JSON.stringify(data),
        processData: false,
        contentType: "application/json",
    })
}