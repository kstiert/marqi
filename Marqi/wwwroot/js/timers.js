const apiBaseUrl = '/v1/timer';

// Initialize Flatpickr for duration input
const durationPicker = flatpickr("#durationPicker", {
  enableTime: true,
  noCalendar: true,
  dateFormat: "H:i:S",
  time_24hr: true,
  defaultHour: 0,
  defaultMinute: 0,
  defaultSeconds: 0
});

// Toggle between duration and date/time inputs
function toggleTimerInputs() {
  const timerType = document.getElementById('timerType').value;
  document.getElementById('durationInputs').style.display = timerType === 'duration' ? 'block' : 'none';
  document.getElementById('datetimeInputs').style.display = timerType === 'datetime' ? 'block' : 'none';
}

// Fetch and display all timers
async function fetchTimers() {
  const response = await fetch(apiBaseUrl);
  const timers = await response.json();
  const timerList = document.getElementById('timerList');
  timerList.innerHTML = ''; // Clear existing timers
  timers.forEach(timer => {
    const endTime = new Date(timer.end); // Parse the End time as a Date object
    const remainingSeconds = Math.max(Math.floor((endTime - new Date()) / 1000), 0); // Calculate remaining seconds
    const timerItem = document.createElement('div');
    timerItem.className = 'timer-item';
    timerItem.innerHTML = `
      <span>${timer.name} - <span id="remaining-${timer.name}">${formatRemainingTime(remainingSeconds)}</span></span>
      <button onclick="cancelTimer('${timer.name}')">Cancel</button>
    `;
    timerList.appendChild(timerItem);
    startCountdown(timer.name, remainingSeconds);
  });
}

// Start a countdown for a timer
function startCountdown(timerName, remainingSeconds) {
  const remainingElement = document.getElementById(`remaining-${timerName}`);
  const interval = setInterval(() => {
    if (remainingSeconds <= 0) {
      clearInterval(interval);
      remainingElement.textContent = 'Expired';
    } else {
      remainingSeconds--;
      remainingElement.textContent = formatRemainingTime(remainingSeconds);
    }
  }, 1000);
}

// Format remaining time in HH:MM:SS
function formatRemainingTime(seconds) {
  const hrs = Math.floor(seconds / 3600);
  const mins = Math.floor((seconds % 3600) / 60);
  const secs = seconds % 60;
  return `${hrs.toString().padStart(2, '0')}:${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
}

// Create a new timer
async function createTimer() {
  const name = document.getElementById('timerName').value;
  const timerType = document.getElementById('timerType').value;

  if (!name) {
    Swal.fire('Error', 'Please provide a timer name.', 'error');
    return;
  }

  let queryParams = `name=${encodeURIComponent(name)}`;

  if (timerType === 'duration') {
    const duration = document.getElementById('durationPicker').value;

    if (!duration) {
      Swal.fire('Error', 'Please select a valid duration.', 'error');
      return;
    }

    queryParams += `&time=${encodeURIComponent(duration)}`;
  } else if (timerType === 'datetime') {
    const date = document.getElementById('date').value;
    const time = document.getElementById('time').value;

    if (!date || !time) {
      Swal.fire('Error', 'Please provide both date and time.', 'error');
      return;
    }

    queryParams += `&end=${encodeURIComponent(date + 'T' + time)}`;
  }

  const response = await fetch(`${apiBaseUrl}/create?${queryParams}`, { method: 'GET' });
  if (response.ok) {
    Swal.fire('Success', 'Timer created successfully!', 'success');
    fetchTimers();
  } else {
    Swal.fire('Error', 'Failed to create timer.', 'error');
  }
}

// Cancel a timer
async function cancelTimer(timerName) {
  const response = await fetch(`${apiBaseUrl}/cancel?name=${encodeURIComponent(timerName)}`, { method: 'GET' });
  if (response.ok) {
    Swal.fire('Success', 'Timer canceled successfully!', 'success');
    fetchTimers();
  } else {
    Swal.fire('Error', 'Failed to cancel timer.', 'error');
  }
}

// Initial fetch of timers
fetchTimers();