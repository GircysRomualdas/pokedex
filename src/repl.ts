import * as readline from "readline";

export function startREPL() {
  const readLine = readline.createInterface({
    input: process.stdin,
    output: process.stdout,
    prompt: "Pokedex > ",
  });

  readLine.prompt();
  readLine.on("line", (line: string) => {
    const cleanLines = cleanInput(line);

    if (cleanLines.length > 0) {
      console.log(`Your command was: ${cleanLines[0]}`);
    }

    readLine.prompt();
  });
}

export function cleanInput(input: string): string[] {
  return input
    .toLowerCase()
    .trim()
    .split(" ")
    .filter((word) => word !== "");
}
