function readRunewordFile() {
    fetch('../../runes.txt')
    .then((reader) => {
        console.log(reader);
    })
    .then((data) => {
        console.log(data);
    });
}

function RuneFileViewer() {
    readRunewordFile();

    return (
      <div >
  
      </div>
    );
  }
  
  export default RuneFileViewer;
  