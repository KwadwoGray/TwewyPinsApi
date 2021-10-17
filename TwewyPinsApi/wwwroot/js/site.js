const uri = 'api/pins';
let pins = [];

function getPins() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayPins(data))
        .catch(error => console.error('Unable to get pins.', error));
}

function addPin() {
    const addNameTextbox = document.getElementById('add-name');

    const pin = {
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(pin)
    })
        .then(response => response.json())
        .then(() => {
            getPins();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add pin.', error));
}

function deletePin(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getPins())
        .catch(error => console.error('Unable to delete pin.', error));
}

function displayEditForm(id) {
    const pin = pins.find(pin => pin.id === id);

    document.getElementById('edit-name').value = pin.name;
    document.getElementById('edit-pinNo').value = item.pinNo;
    document.getElementById('edit-pinUser').value = item.pinUser;
    document.getElementById('edit-info').value = item.info;
    document.getElementById('editForm').style.display = 'block';

}

function updatePin() {
    const pinId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(pinId, 10),
        name: document.getElementById('edit-name').value.trim(),
        pinNo: document.getElementById('edit-pinNo').value.trim(),
        pinUser: document.getElementById('edit-pinUser').value.trim(),
        info: document.getElementById('edit-info').value.trim()
    };

    fetch(`${uri}/${pinId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(pin)
    })
        .then(() => getPins())
        .catch(error => console.error('Unable to update pin.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(pinCount) {
    const name = (pinCount === 1) ? 'pin' : 'pins';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayPins(data) {
    const tBody = document.getElementById('pins');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(pin => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${pin.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deletePin(${pin.id})`);

        let tr = tBody.insertRow();


        let td2 = tr.insertCell(0);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(1);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(2);
        td4.appendChild(deleteButton);
    });

    pins = data;
}