import random
import matplotlib.pyplot as plt
#pip install matplotlib
def play_roulette():
    # Simulate a single spin of the roulette wheel
    chosen_number = random.randint(0, 37)  
    winning_number = random.randint(0, 37)

    if chosen_number == winning_number:
        # If the chosen number matches the winning number, the player wins
        return 36  # 35 times the amount bet + the original bet
    else:
        # If the chosen number doesn't match, the player loses
        return -1  # The player loses the bet

def simulate_roulette(num_spins):
    results = []
    balance = 0
    wins = 0

    for i in range(num_spins):
        result = play_roulette()
        balance += result
        if result == 36:
            wins += 1 
        results.append(balance)

    return results,wins

def plot_results(results,wins):
    # Plot the results of the roulette simulation
    plt.plot(results)
    plt.xlabel(f"Number of Spins: wins: {wins}")
    plt.ylabel("Balance")
    plt.title("Roulette Simulation")
    plt.grid(True)
    plt.show()

if __name__ == "__main__":
    wins = 0
    num_spins = 500  
    simulation_results,wins = simulate_roulette(num_spins)
    plot_results(simulation_results,wins)
