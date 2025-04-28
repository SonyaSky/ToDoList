export const deadlineStyle = (task) => {
    if (task.deadline == null) return '';
    const currentDate = new Date();
    const deadlineDate = new Date(task.deadline);
  
    if (!task.isChecked && deadlineDate < currentDate) {
        return 'red'; 
    }

    const timeDifference = deadlineDate - currentDate;
    const threeDaysInMillis = 3 * 24 * 60 * 60 * 1000; 
    if (!task.isChecked && timeDifference > 0 && timeDifference < threeDaysInMillis) {
        return 'orange'; 
    }

    return '';
};