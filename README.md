# Conway's Game of Life

## Here are the steps to the solution. 

As I had never heard of Game of Life, at first I start researching about the Game. After understanding the rules, I started the implementation.
  
- Implementing
     - First, wrote down the example in the test's PDF and tried to "run" one tick in my handbook, so I would now the expected result after one tick.
     - Then, in code, I created a Hashset of a record struct Cell (with only X and Y props).
        - Hashsets because it hashes the values of the list, so would be faster to look for Cells (O(1)). Also, allows the amount of cells to grow without pre-allocating a 2D grid 64 bits integer array.
        - And record struct because it's a simple structure, Value Type and compares values, and I figured that I would need to compare values instead of reference.
        
- Refactoring
  - Implemented in a non-simultaneous way first, because that would be easier.
    - But then corrected to be simultaneous, as the original implementation (according to the rules on Wikipedia, "Births and Deaths should occurs at the same time")
  - Created the file reader.
    
- Testing 
  - Looked for an simulator that accepted the 1.06 format. Didn't found any simple, so asked claude.ai for a place to test it.
    - Unforunately, I couldn't rely on him to test my input itself (I even tried, but even with his given output coming to be later proved right, that was not enough)
  - So I asked claude.ai to give me websites to test my inputs and surprisingly he found me this: https://copy.sh/life/.
  - I tested few inputs, all inputs that after some ticks, the value would not change anymore, so I would know that the code was right. All passed.
