// ini adalah sebuah object
const animals = [
    { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
    { name: "Carlo", species: "dog", class: { name: "vertebrata" } },
    { name: "Nemo", species: "fish", class: { name: "mamalia" } },
    { name: "Hamilton", species: "dog", class: { name: "mamalia" } },
    { name: "Dory", species: "fish", class: { name: "mamalia" } },
    { name: "Ursa", species: "cat", class: { name: "mamalia" } },
    { name: "Taro", species: "cat", class: { name: "vertebrata" } }
];

// menampilkan species "cat"
for (let i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        console.log(animals[i]);
    }
}

// manipulasi string
for (let i = 0; i < animals.length; i++) {
    if (animals[i].class.name == "mamalia") {
        console.log(animals[i].name+' = Mamalia');
    } else {
        console.log(animals[i].name + ' = Non-Mamalia');
    }
}