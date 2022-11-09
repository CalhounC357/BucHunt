// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/*
 * Get the user's location async.
 * On success, callbackSuccess is called with a GeolocationCoordinates object describing the coordinates of the user at highest
 * accuracy available. On failure, callbackError is called with an error code describing why the call failed.
 * Error codes:
 * 1 - Device does not support geolocation
 * 2 - User denied geolocation permission
 * 3 - Not enough functioning geolocators
 * 4 - Call timed out before data could be acquired
 */
function getLocationAsync(callbackSuccess,callbackError) {
    var geoapi = navigator.geolocation;
    if (geoapi === undefined) {
        setTimeout(() => callbackError(1), 0); // device does not support
    } else {
        // Use a 5 second timeout
        geoapi.getCurrentPosition(callWithLocationSuccess, callWithLocationFailure, { enableHighAccuracy: true, timeout: 5000 });
    }
    // CALLBACKS/PRIVATE
    function callWithLocationSuccess(gpLocation) {
        var coords = gpLocation.coords;
        console.log('success');
        setTimeout(() => callbackSuccess(coords), 0); // success
    }

    function callWithLocationFailure(gpError) {
        switch (gpError.code) {
            case 1: // permission denied
                setTimeout(() => callbackError(2), 0);
                break;
            case 2: // not enough locators
                setTimeout(() => callbackError(3), 0);
                break;
            case 3: // timed out
                setTimeout(() => callbackError(4), 0);
                break;
        }
    }
}

/*
 * Given a coords object, and the decimal forms of the target's latitude and longitude,
 * determines the player distance to the target in metres.
 */
function distanceToLocation(coords, targetLat, targetLon) {
    var playerLat = coords.latitude;
    var playerLon = coords.longitude;
    // Turns out this is surprisingly difficult to do. Lat/lon coordinates do not map to linear distances
    // in the obvious sense.

    // This is based on errata from FCC rules on distance measurement for radio stations to
    // avoid interference and is only applicable for distances no larger than 475 km or 295 miles.
    // It is unlikely that a hunt will have distances of this length to where accuracy would be a problem at this extreme.

    // For trig functions, it is not clear whether degrees or radians are in use.
    // The mathematical definition using radians is assumed.

    // https://www.govinfo.gov/content/pkg/CFR-2016-title47-vol4/pdf/CFR-2016-title47-vol4-sec73-208.pdf
    // section 73.209, p. 87

    // calculate middle latitude
    var middleLatitude = (playerLat + targetLat) / 2;
    // calculate kilometres per degree latitude difference for the middle latitude
    var kiloPerDegDiff = 111.13209 - 0.56605 * Math.cos(2 * middleLatitude) + 0.00120 * Math.cos(4 * middleLatitude);
    // calculate North-South distance
    var nsDist = kiloPerDegDiff * (playerLat - targetLat);
    // calculate East-West distance
    var ewDist = kiloPerDegDiff * (playerLon - targetLon);
    // Now take the Pythagorean using these two NS and EW distances giving kilometres
    var distKm = Math.hypot(nsDist, ewDist);
    // Convert to metres
    return distKm * 1000;
}

// Above testing coords
// Ross Hall                36DEG18'00"N 82DEG22'12"W => 36.30000000N 82.37000000W (lossy conversions)
// DM Centre                36DEG18'20"N 82DEG22'18"W => 36.30555556N 82.37166667W
// Google claims 636.57 metres linear
// Manual test with the above numbers is 647.6763 metres for an error of 11.1 metres (very close.)
// Therefore this sanity tests okay at least done by hand in Java.

/*
 * Return a friendly string for a given distance.
 */
function distanceToString(dist) {
    if (dist < 5) {
        return 'Here';
    } else if (dist < 1000) {
        return (dist) + 'm';
    } else {
        return (dist / 1000).toFixed(2) + 'Km'; // 2 digits fractional kilometres
    }
}