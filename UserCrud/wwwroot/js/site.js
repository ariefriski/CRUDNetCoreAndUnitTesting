const uri = 'api/user';

let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addUsernameTextbox = document.getElementById('add-username');
    const addPasswordTextbox = document.getElementById('add-password');
    const addStatusTextbox = document.getElementById('add-status');


    const item = {
        isComplete: false,
        namaLengkap: addNameTextbox.value.trim(),
        username: addUsernameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim(),
        status:addStatusTextbox.value.trim()
    };

    fetch(uri+'/Save', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addUsernameTextbox.value = '';
            addPasswordTextbox.value = '';
            addStatusTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));

    
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.userId === id);

    document.getElementById('edit-namalengkap').value = item.namaLengkap;
    document.getElementById('edit-name').value = item.username;
    document.getElementById('edit-password').value = item.password;
    document.getElementById('edit-status').value = item.status;
    document.getElementById('edit-id').value = item.userId;
    document.getElementById('edit-isComplete').checked = item.isComplete;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        userId: parseInt(itemId, 10),
        isComplete: document.getElementById('edit-isComplete').checked,
        namaLengkap: document.getElementById('edit-namalengkap').value.trim(),
        username: document.getElementById('edit-name').value.trim(),
        password: document.getElementById('edit-password').value.trim(),
        status: document.getElementById('edit-status').value.trim(),


    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'to-do' : 'to-dos';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.isComplete;

        let editButton = button.cloneNode(false);
        editButton.innerHTML = '<i class="fa fa-pen"></i>';
        editButton.classList.add('btn', 'btn-info');
        editButton.setAttribute('onclick', `displayEditForm(${item.userId})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerHTML = '<i class="fa fa-trash"></i>';
        deleteButton.classList.add('btn', 'btn-danger');
        deleteButton.setAttribute('onclick', `deleteItem(${item.userId})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.userId);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.namaLengkap);
        td3.appendChild(textNode2);

        let td4 = tr.insertCell(3);
        let textNode3 = document.createTextNode(item.username);
        td4.appendChild(textNode3);

        let td5 = tr.insertCell(4);
        let textNode4 = document.createTextNode(item.password);
        td5.appendChild(textNode4);

        let td6 = tr.insertCell(5);
        let textNode5 = document.createTextNode(item.status);
        td6.appendChild(textNode5);

        let td7 = tr.insertCell(6);
        td7.appendChild(editButton);

        let td8 = tr.insertCell(7);
        td8.appendChild(deleteButton);
    });

    todos = data;
}