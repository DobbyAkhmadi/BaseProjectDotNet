$(document).ready(function () {
  const userLocation = L.map('userLocation').setView([42.35, -71.08], 10);
  userLocation.locate({
    setView: true,
    maxZoom: 16
  });

  function onLocationFound(e) {
    const radius = e.accuracy;
    L.marker(e.latlng)
      .addTo(userLocation)
      .bindPopup('You are somewhere around ' + radius + ' meters from this point')
      .openPopup();
    L.circle(e.latlng, radius).addTo(userLocation);
  }
  userLocation.on('locationfound', onLocationFound);
  L.tileLayer('https://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a>',
    maxZoom: 18
  }).addTo(userLocation);

});
