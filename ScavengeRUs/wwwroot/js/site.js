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
